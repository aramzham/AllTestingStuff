import numpy as np
import pandas as pd
#ecom = pd.read_csv(r'C:\Users\Aram\Downloads\Ecommerce Purchases')
#print(ecom.columns.values)
#print(['Address', 'Lot', 'AM or PM', 'Browser Info', 'Company',
#       'Credit Card', 'CC Exp Date', 'CC Security Code', 'CC Provider',
#       'Email', 'Job', 'IP Address', 'Language', 'Purchase Price'])

#print(len([_ for _ in filter(lambda x: x[0].split('/')[1]=='25',ecom[['CC Exp Date']].values)]))

#l = list(map(lambda x: x[0].split('@')[1],ecom[['Email']].values))

#from collections import Counter
#print(list(Counter(l))[:5])

import matplotlib as plt
fig = plt.figure()

#from collections import Counter
#print(list(Counter(l))[:5])
sal['BasePay'].fillna(sal['BasePay'].mean())
print(np.average(sal['BasePay']))
print(np.max(sal['OvertimePay']))
print(sal[sal['EmployeeName']=='JOSEPH DRISCOLL'])
len([_ for _ in filter(lambda x: x.index().lower().contains('cheif'),sal['JobTitle'].value_counts())])