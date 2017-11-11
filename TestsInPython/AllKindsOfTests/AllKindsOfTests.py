def __bool__(self):
    from itertools import permutations
    perms = permutations(self.names)
    for variant in perms:
        for i in range(len(variant) - 1):
            if(variant[i].lower()[-1] != variant[i + 1].lower()[0]): break
        else: return True
    return False

print(method( ["ab", "ba", "ef", "fe"] ))
