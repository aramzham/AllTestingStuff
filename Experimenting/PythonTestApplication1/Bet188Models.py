import json

class SelectionModel(object):
    def __init__(self, name="", price=0):
        self.name = name
        self.price = price
        self.handicapSign = None
        self.selectionTypeId = None
        self.kindOct = None

class MarketModel(object):
    def __init__(self, name="", mHandicap=None):
        self.name = name
        self.mHandicap = mHandicap
        self.selections = []
        self.marketTypeId = None
        self.kindOct = None
        self.sequence = None
        self.point_sequence = None
        self.displayKey = None

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
        self.eventScores = None
        self.currentPeriodString = ""
        self.score = None
        self.info = None

class MatchModel(object):
    def __init__(self):
        self.startTime = None
        self.matchMembers = []
        self.markets = []
        self.isNeutralVenue = False
        self.statistics = None
        self.currentTime = ""
        self.isSuspended = False

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
        self.mapId = None

class BookmakerModel(object):
    def toJSON(self):
        return json.dumps(self, default=lambda o: o.__dict__, sort_keys=True)#, indent=4)
    def __init__(self, bookmaker_number, name):
        self.bookmakerNumber = bookmaker_number
        self.name = name
        self.sports = []
        self.parseDuration = 0
        self.creationDate = None

class MarketTypeMapModel(object):
    def __init__(self):
        self.name = ""
        self.sport_id = 0
        self.market_type_id = None
        self.market_local_kind = None
        self.status = None
        self.display_key = None
        self.sequence = None
        self.point_sequence = None
        self.bookmaker_id = None

class SelectionTypeMapModel(object):
    def __init__(self):
        self.name = ""
        self.market_type_id = 0
        self.selection_type_id = None
        self.kind_oct = None