import requests
from Bet188Models import *
import json
import datetime
import math

class GoodwinFootballParser(object):
    """description of class"""
    _headers = {
                  "user-agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36",
                  #"origin":"https://www.goodwinbet.am",
                  #"referer":"https://www.goodwinbet.am/en/live/",
                  #"content-type":"application/json",
                  #"accept":"application/json",
                  #"accept-encoding":"gzip, deflate, br",
                  #"cookie":"__cfduid=dec5656eb8066f257d228ef18d0e802e71540064048; c=d; _ga=GA1.2.1224878946.1540064049; _gid=GA1.2.98603497.1540064049; user_odd=1; upstream=2; PHPSESSID=B79424F178E55DA5368377A1BA; _gat=1",
                  #"x-hola-request-id":"732000",
                  #"x-hola-unblocker-bext":"reqid 732000: before request, send headers",
                  #"content-length":"41"
               }
    _get_event_list_url = "https://www.goodwinbet.am/frontend_api/events/"

    def parse(self):
        try:
            response = requests.post(type(self)._get_event_list_url, data={"lang":"en","service_id":1,"sport_id":1}, timeout=5, headers=type(self)._headers)

            print("end")

        except Exception as ex:
            pass

