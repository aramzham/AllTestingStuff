class Team(object):
    def __init__(self, names):
        self.names = names

    def __bool__(self):
        from itertools import permutations
        perms = permutations(self.names)
        for variant in perms.__iter__():
            for i in range(len(variant) - 1):
                if(variant[i].lower()[-1] != variant[i + 1].lower()[0]): break
            else: return True
        return False

def isCoolTeam(team):
    return bool(Team(team))

print(isCoolTeam(["Sophie", 
 "Edward", 
 "Deb", 
 "Boris", 
 "Stephanie", 
 "Eric", 
 "Charlotte", 
 "Eric", 
 "Charlie"]))
