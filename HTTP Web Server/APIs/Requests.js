function SendReq(_cmdreqno , _iframeid , _rscelbl)
{
    var CMD = _cmdreqno;
    var ifr = _iframeid;
    console.info(ifr);
    var ifra = document.getElementById(ifr);
    var rescode;
    var exp;
    console.info(ifra);
    
    if(CMD == "CMD1" || CMD == "CMD2" || CMD == "CMD3" || CMD == "EXT" || CMD == "CLR1" || CMD == "CLR2" || CMD == "CLR3")
    {
        

        if(CMD == "CMD1")
        {
            rescode = " "
            var req = new XMLHttpRequest();
            req.abort();
            req.open("GET" , "/CMDOUT/CMD1_Output.txt" , false);
            req.setRequestHeader(CMD, '');
            req.send();
            rescode = req.statusText;
            console.error(req.UNSENT);
            var headers = req.getAllResponseHeaders().toLowerCase();
            console.warn(headers);
            ifra.src = "/CMDOUT/CMD1_Output.txt";
        }

        if(CMD == "CMD2")
        {
            rescode = " "
            var req = new XMLHttpRequest();
            req.abort();
            req.open('GET' , "/CMDOUT/CMD2_Output.txt" , false);
            req.setRequestHeader(CMD, '');
            req.send();
            rescode = req.statusText;
            console.error(req.UNSENT);
            var headers = req.getAllResponseHeaders().toLowerCase();
            console.warn(headers);
            ifra.src = "/CMDOUT/CMD2_Output.txt";
        }

        if(CMD == "CMD3")
        {
            rescode = " "
            var req = new XMLHttpRequest();
            req.abort();
            req.open('GET' , "/CMDOUT/CMD3_output.txt" , false);
            req.setRequestHeader(CMD, '');
            req.send();
            rescode = req.statusText;
            console.error(req.UNSENT);
            var headers = req.getAllResponseHeaders().toLowerCase();
            console.warn(headers);
            ifra.src = "/CMDOUT/CMD3_output.txt";

        }

        if(CMD == "EXT")
        {
            rescode = " "
            var req = new XMLHttpRequest();
            req.abort();
            req.open('GET' , "/CMDOUT/CMD3_output.txt" , false);
            req.setRequestHeader(CMD, '');
            req.send();
            rescode = req.statusText;
            console.error(req.UNSENT);
            var headers = req.getAllResponseHeaders().toLowerCase();
            console.warn(headers);
        }

        if(CMD == "CLR1")
        {
            var req = new XMLHttpRequest();
            req.abort();
            req.open('GET' , "/CMDOUT/CMD3_output.txt" , false);
            req.setRequestHeader(CMD, '');
            req.send();
            rescode = req.statusText;
            console.error(req.UNSENT);
            var headers = req.getAllResponseHeaders().toLowerCase();
            console.warn(headers);
            ifra.src = "/CMDOUT/CMD1_output.txt";

        }

        if(CMD == "CLR2")
        {
            var req = new XMLHttpRequest();
            req.abort();
            req.open('GET' , "/CMDOUT/CMD3_output.txt" , false);
            req.setRequestHeader(CMD, '');
            req.send();
            rescode = req.statusText;
            console.error(req.UNSENT);
            var headers = req.getAllResponseHeaders().toLowerCase();
            console.warn(headers);
            ifra.src = "/CMDOUT/CMD2_output.txt";

        }

        if(CMD == "CLR3")
        {
            var req = new XMLHttpRequest();
            req.abort();
            req.open('GET' , "/CMDOUT/CMD3_output.txt" , false);
            req.setRequestHeader(CMD, '');
            req.send();
            rescode = req.statusText;
            console.error(req.UNSENT);
            var headers = req.getAllResponseHeaders().toLowerCase();
            console.warn(headers);
            ifra.src = "/CMDOUT/CMD3_output.txt";

        }
        
        
        console.info(CMD + " request sucessfully made");
        var exp = document.getElementById(_rscelbl);
        exp.innerHTML = rescode;
        
    }
    else
    {
        console.error(CMD + " Requests are not implemented in server");
    }
    
    
}