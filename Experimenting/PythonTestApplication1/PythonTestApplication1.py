import Bet188Parser
import time
#import win32serviceutil
#import win32service
#import win32event
#import servicemanager
#import socket


#class AppServerSvc (win32serviceutil.ServiceFramework):
#    _svc_name_ = "TestService"
#    _svc_display_name_ = "Test Service"

#    def __init__(self,args):
#        win32serviceutil.ServiceFramework.__init__(self,args)
#        self.hWaitStop = win32event.CreateEvent(None,0,0,None)
#        socket.setdefaulttimeout(60)

#    def SvcStop(self):
#        self.ReportServiceStatus(win32service.SERVICE_STOP_PENDING)
#        win32event.SetEvent(self.hWaitStop)

#    def SvcDoRun(self):
#        servicemanager.LogMsg(servicemanager.EVENTLOG_INFORMATION_TYPE,
#                              servicemanager.PYS_SERVICE_STARTED,
#                              (self._svc_name_,''))
#        self.main()

#    def main(self):
#        _188Bet_parser = Bet188Parser.Bet188Parser()
#        count = 0
#        while True:        
#            bookie = _188Bet_parser.parse()
#            if bookie:
#                bookmaker_json = bookie.toJSON()
#                try:
#                    f = open(r"E:\test\bet188ParsedFile_{0}.txt".format(str(count)), "w")
#                    f.write(bookmaker_json)
#                except :
#                    pass
#            time.sleep(2)
#            count += 1

if __name__ == '__main__':
    win32serviceutil.HandleCommandLine(AppServerSvc)

#if __name__ == "__main__": main()

