#import matplotlib as plt
#import numpy as np
#arr = np.array([64,68,68,70,71,71,79,82,87,92,92,94,96,96])
#plt.pyplot(arr)
#plt.show()


#import numpy as np
#np.zeros(10)
#theta_0 = 15
#theta_1 = 0.2
#variance=1.2
#X_0 = np.random.randint(-50,50,100)
#print(np.random.normal(loc=theta_0+theta_1*X_0))
#print(X_0)
#print(type(X_0))
#if(type(X_0) is not np.ndarray): X_0.ravel()
#else: print('hello')

import numpy as np
import pandas as pd 
ecom = pd.read_csv(r'C:\Users\aram.zhamkochyan\Downloads\Ecommerce Purchases')
print(list(ecom.columns.values))
print(ecom.apply(['Language']).count())

