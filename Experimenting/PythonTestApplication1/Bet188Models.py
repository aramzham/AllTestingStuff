import json

class SelectionModel(object):
	def __init__(self, name="", price=0):
		self.name = name
		self.price = price
		self.handicapSign = None

class MarketModel(object):
	def __init__(self, name="", mHandicap=None):
		self.name = name
		self.mHandicap = mHandicap
		self.selections = []

class TeamModel(object):
	def __init__(self, name=""):
		self.name = name

class MatchMemberModel(object):
	def __init__(self, isHome=False, team=None):
		self.isHome = isHome
		self.team = team

class ScoreModel(object):
    def __init__(self):
        self.score1 = 0
        self.score2 = 0

class MatchStatModel(object):
    def __init__(self):
        self.period_scores = None
        self.period_string = ""
        self.score = None

class MatchModel(object):
    def __init__(self):
        self.startTime = None
        self.matchMembers = []
        self.markets = []
        self.isNeutralVenue = False
        self.statistics = None
        self.currentTime = ""

class LeagueModel(object):
	def __init__(self, name=""):
		self.name = name
		self.matches = []

class RegionModel(object):
	def __init__(self, name=""):
		self.name = name
		self.leagues = []

class SportModel(object):
	def __init__(self, name=""):
		self.name = name
		self.regions = []

class BookmakerModel(object):
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True)#, indent=4)
    def __init__(self, bookmakerNumber, name):
        self.bookmakerNumber = bookmakerNumber
        self.name = name
        self.sports = []