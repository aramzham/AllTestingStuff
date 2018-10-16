import Bet188Parser
import time
import ParserBase
import RabbitMq.MqManager

import socket
import win32serviceutil
import servicemanager
import win32event
import win32service


class SMWinservice(win32serviceutil.ServiceFramework):
    '''Base class to create winservice in Python'''

    _svc_name_ = 'testPythonService'
    _svc_display_name_ = 'Test Python Service'
    _svc_description_ = 'Test Python Service Description'

    @classmethod
    def parse_command_line(cls):
        '''
        ClassMethod to parse the command line
        '''
        win32serviceutil.HandleCommandLine(cls)

    def __init__(self, args):
        '''
        Constructor of the winservice
        '''
        win32serviceutil.ServiceFramework.__init__(self, args)
        self.hWaitStop = win32event.CreateEvent(None, 0, 0, None)
        socket.setdefaulttimeout(60)

    def SvcStop(self):
        '''
        Called when the service is asked to stop
        '''
        self.stop()
        self.ReportServiceStatus(win32service.SERVICE_STOP_PENDING)
        win32event.SetEvent(self.hWaitStop)

    def SvcDoRun(self):
        '''
        Called when the service is asked to start
        '''
        self.start()
        servicemanager.LogMsg(servicemanager.EVENTLOG_INFORMATION_TYPE,
                              servicemanager.PYS_SERVICE_STARTED,
                              (self._svc_name_, ''))
        self.main()

    def start(self):
        '''
        Override to add logic before the start
        eg. running condition
        '''
        self.isRunning = True

    def stop(self):
        '''
        Override to add logic before the stop
        eg. invalidating running condition
        '''
        self.isRunning = False

    def main(self):
        '''
        Main class to be ovverridden to add logic
        '''
        while self.isRunning:
            try:
                parser_188 = Bet188Parser.BetBet188Parser()
                parser_188.initialize()
                mq_manager_188 = RabbitMq.MqManager.MessageQueueManager()
                while True:
                    try:
                       bookmaker_188 = parser_188.parse()
                       bookmaker_188_json = bookmaker_188.toJSON()
                       mq_manager.produce('ParseInfo', bookmaker_188_json, 'Bookmaker_188')
                       time.sleep(2)
                    except Exception as ex:
                        pass   # need to log             
            except :
                pass
            finally :
                mq_manager_188.dispose()

# entry point of the module: copy and paste into the new module
# ensuring you are calling the "parse_command_line" of the new created class
if __name__ == '__main__':
    SMWinservice.parse_command_line()

#def main():
#    try:
#        _188Bet = Bet188Parser.Bet188Parser()
#        _188Bet.initialize()
#        bookmaker = _188Bet.parse()
#        json_bookmaker = bookmaker.toJSON()
#        #f = open(r"D:\Aram\test\bet188ParsedFile.txt", "w")
#        #f.write(json_bookmaker)
#        mq_manager = RabbitMq.MqManager.MessageQueueManager()
#        mq_manager.produce('ParseInfo', json_bookmaker, 'Bookmaker_1888')
#    except Exception as ex:
#        pass

#if __name__ == "__main__": main()

