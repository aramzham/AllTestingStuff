import requests
from bs4 import BeautifulSoup
import json
import time

page_link ='https://landing-sb.prdasbb18a1.com/en-gb/Service/CentralService?GetData&ts={timestamp}'
payload = {'IsFirstLoad': 'true'}
headers = {'content-type': 'application/json', 'user-agent' : 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36', 'Origin':'https://landing-sb.prdasbb18a1.com', 'Referer':'https://landing-sb.prdasbb18a1.com/en-gb/sports/all/in-play?q=&country=AM&currency=USD&tzoff=-240&allowRacing=false&reg=ROE&rc=', 'Content-Type':'application/x-www-form-urlencoded; charset=UTF-8'}
# fetch the content from url
page_response = requests.post(page_link.replace('{timestamp}', str(int(time.time()))), timeout=5, headers=headers, data='IsFirstLoad=true&VersionL=-1&VersionU=0&VersionS=-1&VersionF=-1&VersionH=0&VersionT=-1&IsEventMenu=false&SportID=1&CompetitionID=-1&reqUrl=%2Fen-gb%2Fsports%2Fall%2Fin-play%3Fq%3D%26country%3DAM%26currency%3DUSD%26tzoff%3D-240%26allowRacing%3Dfalse%26reg%3DROE%26rc%3D&oIsInplayAll=false&oIsFirstLoad=true&oSortBy=1&oOddsType=0&oPageNo=0&LiveCenterEventId=0&LiveCenterSportId=0')

print(page_response.headers)
# parse html
page_content = BeautifulSoup(page_response.content, "html.parser")

# extract all html elements where price is stored
prices = page_content.find_all(class_='mic-result-wrapper')
# prices has a form:
#[<div class="main_price">Price: $66.68</div>,
# <div class="main_price">Price: $56.68</div>]

# you can also access the main_price class by specifying the tag of the class
prices = page_content.find_all('div', attrs={'class':'mic-result-wrapper'})
