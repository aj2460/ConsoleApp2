import sys
from nsetools import Nse
nse=Nse()
s=sys.argv[1]
#s="TATAMOTORS"
def qoute(s):
    #l=[]
    q=nse.get_quote(s)
    #l.append(q['lastPrice'])
    #return (l)
    return q['lastPrice']

print(qoute(s))
