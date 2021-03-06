﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Software.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        #modalContainer {
        background-color:rgba(0, 0, 0, 0.3);
        position:absolute;
        width:100%;
        height:100%;
        top:0px;
        left:0px;
        z-index:10000;
        background-image:url(tp.png); /* required by MSIE to prevent actions on lower z-index elements */
    }

    #alertBox {
        position:relative;
        width:38%;
        height:auto;
        margin-top:150px;
        border:1px solid #666;
        background-color:#fff;
        background-repeat:no-repeat;
        background-position:20px 30px;
    }

    #modalContainer > #alertBox {
        position:fixed;
    }

    #alertBox h1 {
        margin:0;
        font:bold 0.9em verdana,arial;
        background-color:#3073BB;
        color:#FFF;
        border-bottom:1px solid #000;
        padding:2px 0 2px 5px;
    }

    #alertBox p {
        font:14px verdana,arial;
        height:auto;
        padding-left:5px;
        width:90%;
        margin:auto;
        margin-top:25px;
        margin-bottom:25px;
    }

    #alertBox #closeBtn {
        display:block;
        position:relative;
        margin:50px auto;
        margin:5px auto;
        padding:7px;
        border:0 none;
        width:70px;
        font:0.7em verdana,arial;
        text-transform:uppercase;
        text-align:center;
        color:#FFF;
        background-color:#357EBD;
        border-radius: 3px;
        text-decoration:none;
    }

    /* unrelated styles */

    #mContainer {
        position:relative;
        width:600px;
        margin:auto;
        padding:5px;
        border-top:2px solid #000;
        border-bottom:2px solid #000;
        font:0.7em verdana,arial;
    }

    h1,h2 {
        margin:0;
        padding:4px;
        font:bold 1.5em verdana;
        border-bottom:1px solid #000;
    }

    code {
        font-size:1.2em;
        color:#069;
    }

    #credits {
        position:relative;
        margin:25px auto 0px auto;
        width:350px; 
        font:0.7em verdana;
        border-top:1px solid #000;
        border-bottom:1px solid #000;
        height:90px;
        padding-top:4px;
    }

    #credits img {
        float:left;
        margin:5px 10px 5px 0px;
        border:1px solid #000000;
        width:80px;
        height:79px;
    }

    .important {
        background-color:#F5FCC8;
        padding:2px;
    }

    code span {
        color:green;
    }
    </style>

    <script type="text/javascript">
        var ALERT_TITLE = "Error Found...!";
        var ALERT_BUTTON_TEXT = "Close";

        if (document.getElementById) {
            window.alert = function (txt) {
                createCustomAlert(txt);
            }
        }

        function createCustomAlert(txt) {
            d = document;

            if (d.getElementById("modalContainer")) return;

            mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
            mObj.id = "modalContainer";
            mObj.style.height = d.documentElement.scrollHeight + "px";

            alertObj = mObj.appendChild(d.createElement("div"));
            alertObj.id = "alertBox";
            if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
            alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
            alertObj.style.visiblity = "visible";

            h1 = alertObj.appendChild(d.createElement("h1"));
            h1.appendChild(d.createTextNode(ALERT_TITLE));

            msg = alertObj.appendChild(d.createElement("p"));
            //msg.appendChild(d.createTextNode(txt));
            msg.innerHTML = txt;

            btn = alertObj.appendChild(d.createElement("a"));
            btn.id = "closeBtn";
            btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
            btn.href = "#";
            btn.focus();
            btn.onclick = function () { removeCustomAlert(); return false; }

            alertObj.style.display = "block";

        }

        function removeCustomAlert() {
            document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
        }
    </script>
    
</head>
<body>
    <center>
        <h1 style="color:Red;">Error Found...!</h1>
    </center>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="ErrorLabel" runat="server"></asp:Label>            
        </div>
    </form>
</body>
</html>
