class SelectionModel(object):
	def __init__(self, name, price, handicapSign):
		self.name = name
		self.price = price
		self.handicapSign = handicapSign        

class MarketModel(object):
	def __init__(self, name, mHandicap, selections):
		self.name = name
		self.mHandicap = mHandicap
		self.selections = selections

class TeamModel(object):
	def __init__(self, name):
		self.name = name

class MatchMemberModel(object):
	def __init__(self, isHome, team):
		self.isHome = isHome
		self.team = team

class MatchModel(object):
	def __init__(self, startTime, matchMembers):
		self.startTime = startTime
		self.matchMembers = matchMembers

class LeagueModel(object):
	def __init__(self, name, matches):
		self.name = name
		self.matches = matches

class RegionModel(object):
	def __init__(self, name, leagues):
		self.name = name
		self.leagues = leagues

class SportModel(object):
	def __init__(self, name, regions):
		self.name = name
		self.regions = regions

class BookmakerModel(object):
	def __init__(self, bookmakerNumber, name, sports):
		self.bookmakerNumber = bookmakerNumber
		self.name = name
		self.sports = sports
