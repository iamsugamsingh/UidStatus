<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StarUID.aspx.cs" Inherits="Software.StarUID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
        #GridView1
        {
            width:100%;    
        }
        .gridrow
        {
            text-align:center;    
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <br />
    <br />
    <center>
    <asp:Label ID="Label1" runat="server" Text="Starred Uids" style="text-align:center; font-size:18px; font-weight:bold;"></asp:Label>
        
    </center>

    <br />
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Starred Uid Available...!" RowStyle-CssClass="gridrow" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White">
            <Columns>
               <asp:TemplateField HeaderText = "Uid" ItemStyle-Width="80px" HeaderStyle-CssClass="xyz" HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <asp:LinkButton ID="uidLinkBtn" runat="server" CommandArgument='<%# Eval("Uid") %>'  OnCommand="Uid_Command" CssClass="LinkStyle" Text='<%# Eval("Uid")%>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Location" DataField="Location"/>               
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
