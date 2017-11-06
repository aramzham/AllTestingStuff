def double_the_vector(vector):
    multiplyer = 2
    return list(map(lambda x: multiplyer * x,vector))

def axpy(a,x,y):
    return [a * x[i] + y[i] for i in range(0,len(x))]

def dot(x,y):
    return sum([x[i] * y[i] for i in range(0,len(x))])

def length(vector):
    return dot(vector,vector) ** 0.5

def algorithm_dot(x,y):
    a = 0
    index = 0
    while index < len(x):
        a += x[index] * y[index]
        index+=1

    return a

def algorithm_dot(a,x,y):
    result_vector = []
    index = 0
    while index < len(x):
        result_vector.append(a * x[index] + y[index])
        index+=1

    return result_vector
    
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

def create_diagonal_matrix(*params):
    if(params == None or len(params) == 0): raise ValueError
    
    matrix = []
    for i in range(len(params)):
        row = []
        for j in range(len(params)):
            if(i == j): row.append(params[i])
            else: row.append(0)
        matrix.append(row)
        del row

    return matrix

# triangular matrix is always square :-)
def create_lower_triangular_matrix(m):
    matrix = []
    for i in range(m):
        row = []
        for j in range(m):
            if(i < j): row.append(0)
            else: row.append(randint(-10,10))
        matrix.append(row)
        del row

    return matrix

def transpose(A):
    if(A == None or len(A) == 0): raise ValueError
    B = []
    for i in range(len(matrix[0])):
        B.append(list(map(lambda x: x[i],matrix)))
    return B

def matrix_vector_multiplication(vector, matrix):
    resultMatrix = []
    for i in range(len(matrix)):
        newRow = []
        sum = 0
        for j in range(len(matrix[i])):
            sum += matrix[i][j] * vector[j][0]
        newRow.append(sum)
        resultMatrix.append(newRow)
        del newRow

    return resultMatrix

# is it right?
def traigular_matrix_vector_multiplication(matrix, vector):
    resultMatrix = []
    for i in range(len(matrix)):
        newRow = []
        sum = 0
        for j in range(len(matrix[i])):
            if(matrix[i][j] != 0): sum += matrix[i][j] * vector[j][0]
        newRow.append(sum)
        resultMatrix.append(newRow)
        del newRow

    return resultMatrix

def matrix_multiplication(matrix1, matrix2):
    if(len(matrix1)<1 or len(matrix1[0])!=len(matrix2)): raise ValueError

    matrix = []
    for i in range(len(matrix1)):
        row = []
        for j in range(len(matrix2[0])):
            row.append(dot(matrix1[i],list(map(lambda x:x[j],matrix2))))
        matrix.append(row)
        del row

    return matrix

def matrix_power(matrix,n):
    if(len(matrix)<1 or len(matrix)!=len(matrix[0])): raise ValueError("Matrix must be square!")

    original_matrix = matrix
    for i in range(n):
        matrix = matrix_multiplication(original_matrix, matrix)

    return matrix

def nth_fibonacci(n):
    base_matrix = [[1,1],[1,0]]
    #     n
    #[1,1]   _ [Fib(n+1),Fib(n)]
    #[1,0]   - [Fib(n),Fib(n-1)]
    fib_matrix = matrix_power(base_matrix,n)
    return fib_matrix[0][1]
