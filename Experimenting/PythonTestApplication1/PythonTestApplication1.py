import Bet188Parser
import time

def main():
    _188Bet_parser = Bet188Parser.Bet188Parser()
    count = 0
    while True:        
        bookie = _188Bet_parser.parse()
        if bookie:
            bookmaker_json = bookie.toJSON()
            try:
                f = open(r"E:\test\bet188ParsedFile_{0}.txt".format(str(count)), "w")
                f.write(bookmaker_json)
            except :
                pass
        time.sleep(2)
        count += 1

if __name__ == "__main__": main()

