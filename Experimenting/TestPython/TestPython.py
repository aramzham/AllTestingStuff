def Factorial(n):
    if n==0: return 1
    else: return n * Factorial(n-1)

def CountTrailingZeros(number):
    count = 0
    number = str(number)
    while number[len(number)-1]=='0':
        number = number[:-1]
        count+=1
    return count

zeros = Factorial(100)
zerosCount = CountTrailingZeros(zeros)


print(zerosCount)
