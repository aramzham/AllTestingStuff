def print_matrix(matrix):
    for array in matrix:
        print(array)
        

matrix = [[a] for a in range(1,5)]

print_matrix(matrix)

def double_the_vector(vector):
    multiplyer = 2
    return list(map(lambda x: multiplyer * x,vector))

vector = [1,3,10]
print(double_the_vector(vector))

def axpy(a,x,y):
    return [a * x[i] + y[i] for i in range(0,len(x))]

a = -1
x = [1,34,4]
y = [2,4,6]

print(axpy(a,x,y))

def dot(x,y):
    return sum([x[i] * y[i] for i in range(0,len(x))])

x = [90,30,10,15,5]
y = [1.4,1.2,0.6,0.2,2.0]

print(dot(x,y))

def length(vector):
    return dot(vector,vector) ** 0.5

print(length([1,0,7]))

def algorithm_dot(x,y):
    a = 0
    index = 0
    while index < len(x):
        a += x[index] * y[index]
        index+=1

    return a

x = [2,-9,8]
y = [1,0,-1]

print(algorithm_dot(x,y))

def algorithm_dot(a,x,y):
    result_vector = []
    index = 0
    while index < len(x):
        result_vector.append(a*x[index] + y[index])
        index+=1

    return result_vector

print(algorithm_dot(-1,[2,-1,4,2,1],[1,-2,2,3,-1]))
    