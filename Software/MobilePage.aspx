<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobilePage.aspx.cs" Inherits="Software.MobilePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Label ID="Label1" runat="server" style="z-index: 1; left: 16px; top: 0px; position: absolute" Text="Label"></asp:Label>
    
        <asp:Image ID="Image1" runat="server" 
            style="z-index: 1; left: 0px; position: absolute; height: 523px; width: 482px; top: 22px;" 
            ImageAlign="Middle"/>
    </div>
    </form>
</body>
</html>
