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
        result_vector.append(a * x[index] + y[index])
        index+=1

    return result_vector

print(algorithm_dot(-1,[2,-1,4,2,1],[1,-2,2,3,-1]))
    
from random import *
def create_random_matrix(m,n):
    matrix = []
    for length in range(m):
        row = []
        for width in range(n):
            row.append(randint(-10,10))
        matrix.append(row)
        del row

    return matrix

print(create_random_matrix(5,3))

def set_matrix_to_zero(matrix):
    if(len(matrix) <= 0): raise ValueError
    for i in range(len(matrix[0])):
        for row in matrix:
            row[i] = 0

    return matrix

def set_matrix_to_identity(matrix):
    if(sum(1 for e in filter(lambda row: len(row) != len(matrix),matrix)) > 0): raise ValueError

    for i in range(len(matrix)):
        for j in range(len(matrix[i])):
            if(i == j): matrix[i][j] = 1
            else: matrix[i][j] = 0

    return matrix

matrix = create_random_matrix(3,3)
print(set_matrix_to_identity(matrix))

def create_diagonal_matrix(*params):
    if(params==None or len(params)==0): raise ValueError
    
    matrix = []
    for i in range(len(params)):
        row = []
        for j in range(len(params)):
            if(i==j): row.append(params[i])
            else: row.append(0)
        matrix.append(row)
        del row

    return matrix

print(create_diagonal_matrix(3,2,5,10))

# triangular matrix is always square :-)
def create_lower_triangular_matrix(m):
    matrix = []
    for i in range(m):
        row = []
        for j in range(m):
            if(i<j): row.append(0)
            else: row.append(randint(-10,10))
        matrix.append(row)
        del row

    return matrix

print(create_lower_triangular_matrix(5))

def transpose(A):
    if(A==None or len(A)==0): raise ValueError
    B=[]
    for i in range(len(matrix[0])):
        B.append(list(map(lambda x: x[i],matrix)))
    return B
