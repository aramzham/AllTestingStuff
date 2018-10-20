from Parsers.TotoFootballParser import TotoFootballParser
import smtplib
import time
import random

def main():
    #server = None
    while True:
        try:
            toto_parser = TotoFootballParser()
            #fromaddr = "aram.j90@gmail.com"
            #toaddr = "van19962013@mail.ru"
            #server = smtplib.SMTP('smtp.gmail.com', 587)
            #server.starttls()
            #server.login(fromaddr, "xsmtp587")
            while True:
                try:
                    start_time = time.time()
                    bookmaker_toto = toto_parser.parse()
                    end_time = time.time()
                    bookmaker_toto_json = bookmaker_toto.toJSON()
                    #server.sendmail(fromaddr, toaddr, bookmaker_toto_json)
                    time.sleep(random.uniform(1,3))
                    print("sent " + str(end_time - start_time) + "s")
                except Exception as ex:
                    print(ex)
                    time.sleep(20)
        except Exception as ex:
            print(ex)            
    #finally:
    #    if(server): server.quit()
if __name__ == "__main__": main()
