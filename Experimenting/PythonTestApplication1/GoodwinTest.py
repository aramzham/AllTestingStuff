from Parsers.GoodwinFootballParser import GoodwinFootballParser
import requests

from urllib.parse import urlencode
from urllib.request import Request, urlopen



def main():
    response = requests.post(url="https://www.goodwinbet.am/frontend_api/events/", data={"lang":"en","service_id":1,"sport_id":1}, headers={"user-agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36"})
    url = 'https://www.goodwinbet.am/frontend_api/events/' # Set destination URL here
    post_fields = {"lang":"en","service_id":1,"sport_id":1}     # Set POST fields here
    
    request = Request(url, urlencode(post_fields).encode())
    request.add_header('user-agent','Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36')
    json = urlopen(request).read().decode()
    print(json)
    #goodwin_parser = GoodwinFootballParser()
    #bookmaker_goodwin = goodwin_parser.parse()
    #bookmaker_goodwin_json = bookmaker_goodwin.toJSON()
    print("end")

if __name__ == "__main__": main()
