import requests
#from bs4 import BeautifulSoup
import json
import time
import Bet188Models
import math
from ParserBase import *

class Bet188Parser(ParserBase):
    """description of class"""

    _page_link = 'https://sb.188bet.co.uk/en-gb/Service/CentralService?GetData&ts={timestamp}'
    _headers = {
        'content-type': 'application/json', 
        'user-agent' : 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36', 
        'Origin':'https://sb.188bet.co.uk', 
        'Referer':'https://sb.188bet.co.uk/en-gb/sports/all/in-play?q=&country=GB&currency=GBP&tzoff=-240&allowRacing=true&reg=UK&rc=GB&PartnerId=18801', 
        'Content-Type':'application/x-www-form-urlencoded; charset=UTF-8'
        }

    def get_market_handicap(self, s):
        if('/' in s):
            split = s.split('/')
            return math.fabs((float(split[0].replace("-","")) + float(split[1])) / 2)
        else:
            return math.fabs(float(s))

    def set_handicap_markets(self, array, name, isHandicap):
    	markets = []
    	for i in range(1,len(array),8):
    		handicap = self.get_market_handicap(array[i])
    		market = Bet188Models.MarketModel(name, handicap)
    		sel1 = Bet188Models.SelectionModel("{t1} ({h})" if isHandicap else "Over", float(array[i + 4]))
    		if(isHandicap): sel1.handicapSign = (+1,-1)[array[i].startswith('-')]
    		sel2 = Bet188Models.SelectionModel("{t2} ({h})" if isHandicap else "Under", float(array[i + 6]))
    		if(isHandicap): sel2.handicapSign = +1 if sel1.handicapSign == -1 else -1
    		market.selections.extend([sel1, sel2])
    		markets.append(market)
    	return markets

    def set_win_market(self, array, name):
    	sel1 = Bet188Models.SelectionModel("W1", float(array[1]))
    	sel2 = Bet188Models.SelectionModel("W2", float(array[3]))	
    	market = Bet188Models.MarketModel(name, None)
    	market.selections.extend([sel1, sel2])
    	if(len(array) > 4): market.selections.append(Bet188Models.SelectionModel("X", float(array[5])))
    	return market

    def set_bts_market(self, array):
    	sel1 = Bet188Models.SelectionModel(array[0][0], float(array[0][2]))
    	sel2 = Bet188Models.SelectionModel(array[1][0], float(array[1][2]))
    	return Bet188Models.MarketModel("Both teams to score",None)

    def parse(self):
        try:
            page_response = requests.post(type(self)._page_link.replace('{timestamp}', str(int(time.time()))), 
                                          timeout=5,
                                          headers=type(self)._headers,
                                          data='IsFirstLoad=true&VersionL=-1&VersionU=0&VersionS=-1&VersionF=-1&VersionH=0&VersionT=-1&IsEventMenu=false&SportID=1&CompetitionID=-1&reqUrl=%2Fen-gb%2Fsports%2Fall%2Fin-play%3Fq%3D%26country%3DGB%26currency%3DGBP%26tzoff%3D-240%26allowRacing%3Dtrue%26reg%3DUK%26rc%3DGB%26PartnerId%3D18801&oIsInplayAll=false&oIsFirstLoad=true&oSortBy=1&oOddsType=0&oPageNo=0&LiveCenterEventId=0&LiveCenterSportId=0')
            #IsFirstLoad=true&VersionL=-1&VersionU=0&VersionS=-1&VersionF=-1&VersionH=0&VersionT=-1&IsEventMenu=false&SportID=1&CompetitionID=-1&reqUrl=%2Fen-gb%2Fsports%2Fall%2Fin-play%3Fq%3D%26country%3DGB%26currency%3DGBP%26tzoff%3D-240%26allowRacing%3Dtrue%26reg%3DUK%26rc%3DGB%26PartnerId%3D18801&oIsInplayAll=false&oIsFirstLoad=true&oSortBy=1&oOddsType=0&oPageNo=0&LiveCenterEventId=0&LiveCenterSportId=0 # new 
            #IsFirstLoad=true&VersionL=-1&VersionU=0&VersionS=-1&VersionF=-1&VersionH=0&VersionT=-1&IsEventMenu=false&SportID=1&CompetitionID=-1&reqUrl=%2Fen-gb%2Fsports%2Fall%2Fin-play%3Fq%3D%26country%3DAM%26currency%3DUSD%26tzoff%3D-240%26allowRacing%3Dfalse%26reg%3DROE%26rc%3D&oIsInplayAll=false&oIsFirstLoad=true&oSortBy=1&oOddsType=0&oPageNo=0&LiveCenterEventId=0&LiveCenterSportId=0 # old
            bet188_json = json.loads(page_response.content)
            bookmaker = Bet188Models.BookmakerModel(188, "188Bet")
            by_sport_data = bet188_json["mod"]["d"]
            for s in by_sport_data:
                if("Darts" in s["n"]): continue
                sport = Bet188Models.SportModel(s["n"])
                region = Bet188Models.RegionModel("World")
                sport.regions.append(region)
                for l in s["c"]:
                    league = Bet188Models.LeagueModel(l["n"])
                    for g in l["e"]:
                        if(g["cei"] and g["cei"]["n"]): continue
                        match_info = g["i"]
                        match_member_1 = Bet188Models.MatchMemberModel(True, Bet188Models.TeamModel(match_info[0]))
                        match_member_2 = Bet188Models.MatchMemberModel(False, Bet188Models.TeamModel(match_info[1]))
                        match = Bet188Models.MatchModel()
                        match.startTime = g["edt"]
                        match.isNeutralVenue = True if g["g"] == "N" else False
                        match.matchMembers.extend([match_member_1, match_member_2])	
                        
                        #statistics
                        if(match_info[12]):
                            match.statistics = Bet188Models.MatchStatModel()
                            match.statistics.currentPeriodString = match_info[12]
                            if(match_info[10] and match_info[11]):
                                match.statistics.score = Bet188Models.ScoreModel()
                                match.statistics.score.score1 = int(match_info[10])
                                match.statistics.score.score2 = int(match_info[11])
                            if(match_info[8] and match_info[9]):
                                red_cards_score = Bet188Models.ScoreModel()
                                red_cards_score.score1 = int(match_info[8])
                                red_cards_score.score2 = int(match_info[9])
                                match.statistics.eventScores = {2: red_cards_score}
                            if(match_info[5]):
                                match.currentTime = match_info[5]

                        # markets
                        if("o" not in g): continue
                        if("ah" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ah"], "Handicap", isHandicap=True))
                        if("ah1st" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ah1st"], "1st half Handicap", isHandicap=True))
                        if("ahs1" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ahs1"], "Set 1 Handicap", isHandicap=True))
                        if("ahs2" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ahs2"], "Set 2 Handicap", isHandicap=True))
                        if("ahs3" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ahs3"], "Set 3 Handicap", isHandicap=True))
                        if("ahs4" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ahs4"], "Set 4 Handicap", isHandicap=True))
                        if("ahpt" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ahpt"], "Points Handicap", isHandicap=True))
                        if("ou" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ou"], "Over/Under", isHandicap=False))
                        if("ou1st" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ou1st"], "1st half Over/Under", isHandicap=False))
                        if("ous1" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ous1"], "Set 1 Over/Under", isHandicap=False))
                        if("ous2" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ous2"], "Set 2 Over/Under", isHandicap=False))
                        if("ous3" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ous3"], "Set 3 Over/Under", isHandicap=False))
                        if("ous4" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ous4"], "Set 4 Over/Under", isHandicap=False))
                        if("ous4" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ous4"], "Set 4 Over/Under", isHandicap=False))
                        if("ouq1" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ouq1"], "Quarter 1 Over/Under", isHandicap=False))
                        if("ouq2" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ouq2"], "Quarter 2 Over/Under", isHandicap=False))
                        if("ouq3" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ouq3"], "Quarter 3 Over/Under", isHandicap=False))
                        if("ouq4" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["ouq4"], "Quarter 4 Over/Under", isHandicap=False))
                        if("oupt" in g["o"]): match.markets.extend(self.set_handicap_markets(g["o"]["oupt"], "Points Over/Under", isHandicap=False))
                        if("1x2" in g["o"]): match.markets.append(self.set_win_market(g["o"]["1x2"], "1X2"))
                        if("1x21st" in g["o"]): match.markets.append(self.set_win_market(g["o"]["1x21st"], "1st half 1X2"))
                        if("ml" in g["o"]): match.markets.append(self.set_win_market(g["o"]["ml"], "Money Line"))
                        if("mls1" in g["o"]): match.markets.append(self.set_win_market(g["o"]["mls1"], "Set 1 Money Line"))
                        if("mls2" in g["o"]): match.markets.append(self.set_win_market(g["o"]["mls2"], "Set 2 Money Line"))
                        if("mls3" in g["o"]): match.markets.append(self.set_win_market(g["o"]["mls3"], "Set 3 Money Line"))
                        if("mls4" in g["o"]): match.markets.append(self.set_win_market(g["o"]["mls4"], "Set 4 Money Line"))
                        if("bts" in g["o"]): match.markets.append(self.set_bts_market(g["o"]["bts"]))
                        # TODO: Add all other markets
                        league.matches.append(match)
                    region.leagues.append(league)
                bookmaker.sports.append(sport)
            self.validate(bookmaker)
            return bookmaker
        except Exception as ex:
            pass


