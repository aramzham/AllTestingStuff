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

def almostIncreasingSequence(sequence):
    if(isStricktlyIncreasing(sequence)): return True
    initial_sequence = sequence[:]
    for i in range(len(initial_sequence)):
        del sequence[i]
        if(isStricktlyIncreasing(sequence)): return True
        sequence = initial_sequence[:]
    return False

def isStricktlyIncreasing(sequence):
    for i in range(len(sequence)-1):
        if(sequence[i]>=sequence[i+1]): return False
    return True

print(almostIncreasingSequence([1, 2, 3, 4, 3, 6]))