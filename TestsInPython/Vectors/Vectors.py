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

x = [1,2,3]
y = [1,3,-2]

print(dot(x,y))

def length(vector):
    return dot(vector,vector) ** 0.5

print(length([1,0,7]))