import requests
from Bet188Models import *
import json
import datetime
import math

class TotoFootballParser(object):
    """description of class"""
    _headers = {
                  "user-agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100Safari/537.36",
                  "origin":"https://sport.totogaming.am",
                  #"content-type":"application/json;",
                  #"accept":"application/json, text/javascript, */*; q=0.01"
               }
    _get_event_list_url = "https://sport.totogaming.am/InPlay/GetEventsList"

    def parse(self):
        try:
            response = requests.post(type(self)._get_event_list_url,data={"sportId":1,"langId":2,"partnerId":1,"stTypes":[1,37,2532,2533,26,46,992]}, timeout=5, headers=type(self)._headers)
            # dc = 37, ah = 2532, asian total = 2533, t1 total = 69, t2 total = 75, bts = 26
            if(not response): return None
            toto_json = json.loads(response.content)

            bookmaker = BookmakerModel(8, "TotoFootball")
            for m in toto_json:
                sport = None
                existingSports = list(filter(lambda x: x.name == m["SN"], bookmaker.sports))
                if(len(existingSports) == 0):
                    sport = SportModel(name=m["SN"])
                    bookmaker.sports.append(sport)
                else:
                    sport = existingSports[0]

                region = None
                existingRegions = list(filter(lambda x: x.name == m["CtN"], sport.regions))
                if(len(existingRegions) == 0):
                    region = RegionModel(name=m["CtN"])
                    sport.regions.append(region)
                else:
                    region = existingRegions[0]

                league = None
                existingLeagues = list(filter(lambda x: x.name == m["CN"], region.leagues))
                if(len(existingLeagues) == 0):
                    league = LeagueModel(name=m["CN"])
                    region.leagues.append(league)
                else:
                    league = existingLeagues[0]

                match = MatchModel()
                match.matchMembers.append(MatchMemberModel(isHome=True,team=TeamModel(m["HT"])))
                match.matchMembers.append(MatchMemberModel(isHome=False,team=TeamModel(m["AT"])))
                match.startTime = datetime.datetime.fromtimestamp(m["TS"] / 1e3).strftime("%D %H:%M")
                match.currentTime = m["PT"]

                match.statistics = MatchStatModel()
                match.statistics.currentPeriodString = m["ES"]
                match.statistics.score = ScoreModel()
                match.statistics.score.score1 = m["HS"]
                match.statistics.score.score2 = m["AS"]
                if(m["YC"] or m["CR"]):
                    match.statistics.eventScores = {}
                    if(m["YC"]):
                        split = m["YC"].split(':')
                        if(len(split) == 2 and split[0].isdigit() and split[1].isdigit()):
                            score_yellow = ScoreModel()
                            score_yellow.score1 = int(split[0])
                            score_yellow.score2 = int(split[1])
                            match.statistics.eventScores['YellowCards'] = score_yellow
                    if(m["CR"]):
                        split = m["CR"].split(':')
                        if(len(split) == 2 and split[0].isdigit() and split[1].isdigit()):
                            score_corner = ScoreModel()
                            score_corner.score1 = int(split[0])
                            score_corner.score2 = int(split[1])
                            match.statistics.eventScores['Corners'] = score_corner
                if(m["SS"]):
                    match.statistics.periodScores = []
                    periods_split = m["SS"].split(' - ')
                    if(len(periods_split) == 2):
                        for period_split in periods_split:
                            split = period_split.split(':')
                            if(len(split) == 2 and split[0].isdigit() and split[1].isdigit()):
                                period_score = ScoreModel()
                                period_score.score1 = int(split[0])
                                period_score.score2 = int(split[1])
                                match.statistics.periodScores.append(period_score)

                if(m["StakeTypes"]):
                    for stake_type in m["StakeTypes"]:
                        if(not stake_type["Stakes"] or len(stake_type["Stakes"]) == 0): continue
                        # win market, double chance
                        if(stake_type["Id"] == 1 or stake_type["Id"] == 26):
                            market = MarketModel(name=stake_type["N"])
                            for stake in stake_type["Stakes"]:
                                sel = SelectionModel(name=stake["N"], price=stake["F"])
                                market.selections.append(sel)
                            if(len(market.selections) > 0): match.markets.append(market)

                        # handicaps
                        elif(stake_type["Id"] == 992 or stake_type["Id"] == 2532):
                            for stake in stake_type["Stakes"]:
                                market = None
                                existingMarkets = list(filter(lambda x: x.mHandicap == math.fabs(stake["A"]) and x.name == stake_type["N"], match.markets))
                                sel = SelectionModel(name="{t1} ({h})", price=stake["F"]) if stake["SC"] == 1 else SelectionModel("{t2} ({h})", stake["F"]) if stake["SC"] == 2 else None
                                if(not sel): continue
                                sel.handicapSign = -1 if stake["A"] <= 0 else +1
                                if(len(existingMarkets) == 0):
                                    market = MarketModel(name=stake_type["N"], mHandicap=math.fabs(stake["A"]))                                   
                                    market.selections.append(sel)
                                    match.markets.append(market)
                                elif(len(existingMarkets) == 1):
                                    existingMarkets[0].selections.append(sel)
                                else:
                                    correct_market = list(filter(lambda x: list(filter(lambda y: y.name != sel.name, x.selections)) > 0, existingMarkets))[0]
                                    correct_market.selections.append(sel)
                        
                        # totals
                        elif(stake_type["Id"] == 46 or stake_type["Id"] == 2533):
                            for stake in stake_type["Stakes"]:
                                market = None
                                existingMarkets = list(filter(lambda x: x.mHandicap == stake["A"] and x.name == stake_type["N"], match.markets))
                                sel = SelectionModel(name="Over", price=stake["F"]) if stake["SC"] == 1 else SelectionModel("Under", stake["F"]) if stake["SC"] == 2 else None
                                if(not sel): continue
                                if(len(existingMarkets) == 0):
                                    market = MarketModel(name=stake_type["N"], mHandicap=stake["A"])                                   
                                    market.selections.append(sel)
                                    match.markets.append(market)
                                elif(len(existingMarkets) == 1):
                                    existingMarkets[0].selections.append(sel)
                league.matches.append(match)
            return bookmaker
        except Exception as ex:
            print(ex)
