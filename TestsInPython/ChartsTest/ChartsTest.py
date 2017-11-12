import numpy as np
import pandas as pd
sal = pd.read_csv(r'C:\Users\Aram\Downloads\Salaries.csv')
print(sal.columns.values)
print(['Id' 'EmployeeName' 'JobTitle' 'BasePay' 'OvertimePay' 'OtherPay'
 'Benefits' 'TotalPay' 'TotalPayBenefits' 'Year' 'Notes' 'Agency' 'Status'])

#print(len([_ for _ in filter(lambda x: x[0].split('/')[1]=='25',ecom[['CC Exp Date']].values)]))

#l = list(map(lambda x: x[0].split('@')[1],ecom[['Email']].values))

#from collections import Counter
#print(list(Counter(l))[:5])
sal['BasePay'].fillna(sal['BasePay'].mean())
print(np.average(sal['BasePay']))
print(np.max(sal['OvertimePay']))
print(sal[sal['EmployeeName']=='JOSEPH DRISCOLL'])
len([_ for _ in filter(lambda x: x.index().lower().contains('cheif'),sal['JobTitle'].value_counts())])