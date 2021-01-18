<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyPage.aspx.cs" Inherits="Software.MyPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UID STATUS</title>
    <link rel="icon" type="image/jpg" href="images/ANU LOGO.jpg">

    <style type="text/css">
        .star
        {
            font-size:xx-large;
            -webkit-text-stroke-width: 0.5px;
            -webkit-text-stroke-color: black;
            display:block;
            float:right;
            margin-right:10px;
            margin-top:3px;
            color:white;
            text-decoration:none;
        }
        #seeStarUids
        {
            display:inline-block;
            text-decoration:none;
            margin-bottom:-40px;    
        }
        
    </style>


    <script type="text/javascript">
        function Download(url) {
            document.getElementById('pdfpath').src = url;
        };
    </script>

    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js"></script>
<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js"></script>
<link rel="Stylesheet" type="text/css" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" />

<script type="text/javascript">
    $(function () {
        $("#custreftxt").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: 'MyPage.aspx/GetCustRef',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item.Text, val: item.Value };
                            }))
                        } else {
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                if (u.item.val == -1) {
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>

<script type="text/javascript">
    $(function () {
        $("#custdrawingtxt").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: 'MyPage.aspx/GetCustDrawing',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item.Text, val: item.Value };
                            }))
                        } else {
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                if (u.item.val == -1) {
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>

<script type="text/javascript">
    $(function () {
        $("#ptnumtxt").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: 'MyPage.aspx/GetPtNum',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item.Text, val: item.Value };
                            }))
                        } else {
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                if (u.item.val == -1) {
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>
    
<script type="text/javascript">
    $(function () {
        $("#articletxt").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: 'MyPage.aspx/ArticleNumber',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item.Text, val: item.Value };
                            }))
                        } else {
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                if (u.item.val == -1) {
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>
    
<script type="text/javascript">
    $(function () {
        $("#uidtxtbox").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: 'MyPage.aspx/GetUidTxt',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item.Text, val: item.Value };
                            }))
                        } else {
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                if (u.item.val == -1) {
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>

<script type="text/javascript">
    $(function () {
        $("#worles_POtxt").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: 'MyPage.aspx/GetWorlesPoTxt',
                    data: "{ 'prefix': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data.d.length > 0) {
                            response($.map(data.d, function (item) {
                                return { label: item.Text, val: item.Value };
                            }))
                        } else {
                            response([{ label: 'No results found.', val: -1}]);
                        }
                    }
                });
            },
            select: function (e, u) {
                if (u.item.val == -1) {
                    $(this).val("");
                    return false;
                }
            }
        });
    });
</script>

<script type = "text/javascript">

    function SetTarget() {

        document.forms[0].target = "_blank";

    }

</script>

</head>

<body id="PageBody" runat="server" style="top: 7px; left: 140px; position: absolute;
    height: 19px; width: 1053px;">
    <form id="form1" runat="server" style="height:1000px; margin-bottom:100px;">
    <asp:Label ID="drawingtypetxt" runat="server" Visible="false"></asp:Label>
    <br>
    <asp:LinkButton ID="seeStarUids" runat="server" OnClick="seeStarUids_Click">See Starred Uids</asp:LinkButton>
    <center>
        <div> 
            <asp:Label ID="uidstatuslbl" style="font-size:24px;" runat="server" 
                Text="UID STATUS" Font-Bold="True" Font-Size="20px" Font-Underline="True"></asp:Label>
        </div>
        </center>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Copyright 2020" Style="float:right; font-weight:bold;margin-top:-20px; text-decoration:underline"></asp:Label>
    </div>
        <br>  
                <asp:Label ID="errorlbl" runat="server" Style="Color:Red; font-weight:bold; margin-left:28px;" Text="Label" Visible="False"></asp:Label>

                <br>
        <div>
            <asp:Panel ID="Panel1" style="width:100%; margin:auto;" runat="server" Height="464px">
            <div style="padding-left:5px; padding-right:5px; width:320px; float:left; margin-top:5px; height:50px;line-height:50px; background:#A3A09F;">
                <asp:Label ID="uidlbl" runat="server" Text="UID" Font-Bold="True" 
                    Font-Size="20px"></asp:Label>
                <asp:TextBox ID="uidtxtbox" runat="server" style="text-align:center;" 
                    Height="30px" Font-Bold="True" Width="100px"></asp:TextBox>
                <asp:Button ID="searchbtn" runat="server" Text="Search" onclick="searchbtn_Click" Font-Bold="True" Height="35px" />
                <asp:Button ID="prevbtn" runat="server" Text="&lt;" onclick="prevbtn_Click" 
                    Height="35px" />
                <asp:Button ID="nextbtn" runat="server" Text="&gt;" onclick="nextbtn_Click" 
                    Height="35px" />

                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="star" OnClick="MarkAsStar_Click">★</asp:LinkButton>

                    </div>

                &nbsp;<asp:LinkButton ID="worles_POLink" runat="server" OnClientClick = "SetTarget();" 
                onclick="worles_POLink_Click">Worles_PO</asp:LinkButton>

                &nbsp;&nbsp;<asp:LinkButton ID="ptnumlink" runat="server" OnClick="ptnumlink_click">PT_Number</asp:LinkButton>
                &emsp;<asp:Label ID="orderdatelbl" runat="server" Text="OrderDate"></asp:Label>
               &emsp;&emsp;<asp:Label ID="deliverydatelbl" runat="server" Text="DeliveryDate"></asp:Label>

                &emsp;&emsp;<asp:Label ID="startdatelbl" runat="server" Text="StartDate"></asp:Label>
                &emsp;&emsp;&emsp;<asp:Label ID="enddatelbl" runat="server" Text="EndDate"></asp:Label>
                &emsp;&nbsp;<asp:Label ID="quantitylbl" runat="server" Text="Qty"></asp:Label>
               &emsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="DQty"></asp:Label>

                <br>
               &emsp;<asp:TextBox ID="worles_POtxt" runat="server" style="margin-left:-10px; text-align:center;" 
                    Width="60px" Font-Bold="True" Height="30px" 
                    ontextchanged="worles_POtxt_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:TextBox ID="ptnumtxt" runat="server" Width="80px" 
                    style="text-align:center;" Font-Bold="True" Height="30px" 
                    ontextchanged="ptnumtxt_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:TextBox ID="orderdatetxt" runat="server" Width="100px" 
                    style="text-align:center;" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="deliverydatetxt" runat="server" Width="100px" 
                    style="text-align:center;" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="startdatetxt" runat="server" Width="100px" 
                    style="text-align:center;" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="enddatetxt" runat="server" Width="80px" 
                    style="text-align:center;" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="quantitytxt" runat="server" Width="40px" 
                    style="text-align:center;" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="deliveredquantitytxt" runat="server" Width="35px" 
                    style="text-align:center;" Font-Bold="True" Height="30px"></asp:TextBox>
                <br>
                <br>
                    <asp:CheckBox ID="CheckBox1" runat="server" /> <asp:Label ID="articlelbl" runat="server" Text="Article"></asp:Label>
                  &emsp;&emsp;&emsp;<asp:Label ID="datoslbl" runat="server" Text="Datos"></asp:Label>
                  &emsp;&emsp;<asp:Label ID="pendingqtylbl" runat="server" Text="Pending Qty"></asp:Label>
                  &emsp;<asp:Label ID="dtolbl" runat="server" Text="COM %"></asp:Label>
                  &emsp;&emsp;&emsp;<asp:Label ID="ratelbl" runat="server" Text="Rate"></asp:Label>
                  &emsp; &emsp;<asp:Label ID="pointslbl" runat="server" Text="Points"></asp:Label>
                  &emsp;&emsp;&emsp;<asp:Label ID="locationlbl" runat="server" Text="Location"></asp:Label>
                  &emsp; &emsp; &emsp;&emsp;&emsp;<asp:LinkButton ID="custreflink" runat="server" OnClientClick = "SetTarget();" onclick="custreflink_Click">Cust. Reference</asp:LinkButton>
                <br>
                <asp:TextBox ID="articletxt" runat="server" style="text-align:center;" 
                    Width="100px" Font-Bold="True" Height="30px" 
                    ontextchanged="articletxt_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:TextBox ID="datostxt" runat="server" style="text-align:center;" 
                    Width="100px" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="pendingqtytxt" runat="server" style="text-align:center;" 
                    Width="100px" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="dtotxt" runat="server" style="text-align:center;" Width="100px" 
                    Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="ratetxt" runat="server" style="text-align:center;" 
                    Width="100px" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="pointstxt" runat="server" style="text-align:center;" 
                    Width="100px" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="locationtxt" runat="server" style="text-align:center;" 
                    Width="100px" Font-Bold="True"
                    Height="32px" BorderStyle="Solid" BorderColor="Gray" BorderWidth="1"></asp:TextBox>
                <asp:TextBox ID="custreftxt" runat="server" style="text-align:center;" 
                    Width="250px" Font-Bold="True" Height="30px" 
                    ontextchanged="custreftxt_TextChanged" AutoPostBack="true"></asp:TextBox>
                <br>
                <br>
                <div style="margin-top:-15px; float:left;">
                <asp:Button ID="back" runat="server" Text="&lt;" Height="25px" onclick="back_Click" />
                <asp:Button ID="forth" runat="server" Text="&gt;" Height="25px" 
                        onclick="forth_Click" />
                </div>
                   <asp:Label ID="descriptionlbl" runat="server" Text="Description"></asp:Label>
                       &emsp;&emsp;&emsp;&emsp;&emsp;<asp:LinkButton ID="RefSteelCutLink" runat="server" OnClick="lnkbtnRefStillCut_Click">Ref.SteelCut</asp:LinkButton>
                         <asp:Label ID="custdrawinglbl" runat="server" Text="Cust.Drawing"></asp:Label>
                         <asp:Label ID="markingonpiecelbl" runat="server" Text="Marking On Piece"></asp:Label>
                        <asp:Label ID="observationlbl" runat="server" Text="Observation"></asp:Label>
                <br>
                <asp:TextBox ID="descriptiontxt" style="text-align:center;" runat="server" 
                    Width="200px" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="refsteelcuttxt" style="text-align:center;" runat="server" 
                    Width="150px" Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="custdrawingtxt" style="text-align:center;" runat="server" 
                    Width="200px" Font-Bold="True" Height="30px" 
                    ontextchanged="custdrawingtxt_TextChanged" AutoPostBack="true"></asp:TextBox>
                <asp:TextBox ID="markingonpiecetxt" style="text-align:center;" runat="server" Width="200px" 
                    Font-Bold="True" Height="30px"></asp:TextBox>
                <asp:TextBox ID="observationtxt" style="text-align:center;" runat="server" 
                    Width="225px" Font-Bold="True" Height="30px"></asp:TextBox>  
                
                <asp:Panel ID="Panel2" runat="server" style="border:2px solid #9A9A9A; width:50%; margin:auto; float:left; margin-top:20px; height:600px;" ScrollBars="Auto">
                    <div style="width:85%; margin:auto; margin-top:10px;">
                        <asp:Button ID="pbtn" runat="server" Text="Pieces" Font-Bold="True" onclick="pbtnbtn_Click"
                            BackColor="#FF9933"/>
                        <asp:Button ID="esbtn" runat="server" Text="External Steps" onclick="esbtnbtn_Click"
                            Font-Bold="True" BackColor="#33CC33"/>
                        <asp:Button ID="pendingbtn" runat="server" Text="Pending" Font-Bold="True" onclick="pendingbtn_Click"
                            BackColor="Red"/>
                        <asp:Button ID="internalstepsbtn" runat="server" Text="Internal Steps" onclick="internalstepsbtn_Click"
                            Font-Bold="True" BackColor="Yellow"/>
                        <asp:Button ID="viewall" runat="server" Text="View All" onclick="viewall_Click" Font-Bold="True" BackColor="#ffffff"/>
                    </div>
                    <center>
                    
                            <asp:GridView ID="GridView1" style="margin-top:25px;" runat="server" 
                                AutoGenerateColumns="false" HeaderStyle-BackColor="#FF9933" 
                                HeaderStyle-BorderColor="Black" Width="100%">  
                                <Columns>  
                                    <asp:BoundField HeaderText="UID" DataField="NumOrd" />  
                                    <asp:BoundField HeaderText="Piece" DataField="CodPie" />    
                                    <asp:BoundField HeaderText="Vend" DataField="ProPie" />  
                                    <asp:BoundField HeaderText="DelDate" DataField="PlaPie" DataFormatString="{0:dd-MM-yyyy}" />  
                                    <asp:BoundField HeaderText="OK" DataField="PieFin" /> 
                                    <asp:BoundField HeaderText="Material" DataField="PreAce" />                                       
                                </Columns>  
                                <HeaderStyle BackColor="#FF9933" BorderColor="Black" Width="50px" 
                                    Font-Size="14px" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="14px" BackColor="White" />
                            </asp:GridView> 

                    <asp:GridView ID="GridView5" style="margin-top:25px;" runat="server" AutoGenerateColumns="False" Width="100%"    > <%--orange Table--%>
                            <Columns>  
                                    <asp:BoundField HeaderText="Piece" DataField="CodPie" />  
                                    <asp:BoundField HeaderText="Description" DataField="NomPie" />    
                                    <asp:BoundField HeaderText="Material" DataField="MatPie" />  
                                    <asp:BoundField HeaderText="Grade" DataField="CalPie"/>  
                                    <asp:BoundField HeaderText="Hardness" DataField="DurPie"/> 
                                    <asp:BoundField HeaderText="OD" DataField="DiaExt" />                                       
                                    <asp:BoundField HeaderText="Length" DataField="Longit" />                                       
                                    <asp:BoundField HeaderText="ID" DataField="DiaInt" />                                       
                                    <asp:BoundField HeaderText="M" DataField="Modpie" />                                       
                                </Columns>  
                                <HeaderStyle BackColor="#FF9933" BorderColor="Black" Width="50px" 
                                    Font-Size="14px" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="14px" BackColor="White"/>
                    </asp:GridView>

               <asp:GridView ID="GridView2" style="margin-top:25px;" runat="server"   AutoGenerateColumns="False" Width="100%"  >
                            <Columns>  
                                    <asp:BoundField HeaderText="UID" DataField="NumOrd" />  
                                    <asp:BoundField HeaderText="StepNo" DataField="NumFas" />    
                                    <asp:BoundField HeaderText="Piece" DataField="CodPie" />  
                                    <asp:BoundField HeaderText="QtyPiece" DataField="CanPie"/>  
                                    <asp:BoundField HeaderText="RecvdDate" DataField="FecAlbExt" DataFormatString="{0:dd-MM-yyyy}" /> 
                                    <asp:BoundField HeaderText="CodProExt" DataField="CodProExt" />                                       
                                    <asp:BoundField HeaderText="Price" DataField="PrePieExt" />  
                                    <asp:TemplateField HeaderText="Time Taken" ControlStyle-Width="3%">    
                                        <ItemTemplate>
                                            <%--<asp:Literal ID="Literal4" runat="server" Text='<%# (System.Convert.ToDateTime(Eval("Horfin"))-System.Convert.ToDateTime(Eval("HorIni")))%>'></asp:Literal> --%>
                                            <asp:Literal ID="Literal4" runat="server" Text='<%# (Eval("FecAlbExt").ToString()!="" && Eval("OrderDate").ToString()!="")? (System.Convert.ToDateTime(Eval("FecAlbExt"))-System.Convert.ToDateTime(Eval("OrderDate"))).ToString():"Order Date Missing" %>'></asp:Literal> 
                                        </ItemTemplate>
                                        <ControlStyle Width="3%" />
                                    </asp:TemplateField>                                     
                                </Columns>  
                                <HeaderStyle BackColor="Green" BorderColor="Black" Width="50px" 
                                    Font-Size="14px" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="14px" BackColor="White"/>
                    </asp:GridView>
                    
                    <asp:GridView ID="GridView3" style="margin-top:25px;" runat="server" AutoGenerateColumns="false" Width="100%"    > 
                                <Columns>  
                                    <asp:BoundField HeaderText="UID" DataField="NumOrd" />  
                                    <asp:BoundField HeaderText="StepNo" DataField="NumFas" />    
                                    <asp:BoundField HeaderText="Start_Time" DataField="HorIni" DataFormatString="{0:dd-MM-yyyy}"/>  
                                    <asp:BoundField HeaderText="End_Time" DataField="Horfin" DataFormatString="{0:dd-MM-yyyy}"/>  
                                    <asp:TemplateField HeaderText="Time_Taken" ControlStyle-Width="3%">    
                                        <ItemTemplate>
                                            <%--<asp:Literal ID="Literal4" runat="server" Text='<%# (System.Convert.ToDateTime(Eval("Horfin"))-System.Convert.ToDateTime(Eval("HorIni")))%>'></asp:Literal> --%>
                                            <asp:Literal ID="Literal4" runat="server" Text='<%# (Eval("HorFin").ToString()!="" && Eval("HorFin").ToString()!="")? (System.Convert.ToDateTime(Eval("Horfin"))-System.Convert.ToDateTime(Eval("HorIni"))).ToString():"00.00.00" %>'></asp:Literal> 
                                        </ItemTemplate>
                                        <ControlStyle Width="3%" />
                                        </asp:TemplateField>
                                    <asp:BoundField HeaderText="OpetrNum" DataField="CodPer" /> 
                                    <asp:BoundField HeaderText="MachinNum" DataField="CodMaq" />                                       
                                    <asp:BoundField HeaderText="QtyPiece" DataField="CanPie" />
                                </Columns>
                                <HeaderStyle BackColor="Yellow" BorderColor="Black" Font-Size="14px" />

                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="12px" 
                                    BackColor="White"/>

                    </asp:GridView>
                
                    <asp:GridView ID="GridView4" style="margin-top:25px;" runat="server"  AutoGenerateColumns="false" Width="100%"  >
                            <Columns>  
                                    <asp:BoundField HeaderText="Step" DataField="NumFas" />  
                                    <asp:BoundField HeaderText="Piece" DataField="CodPie" />    
                                    <asp:BoundField HeaderText="OrdNum" DataField="NumPed"/>  
                                    <asp:BoundField HeaderText="OrderDate" DataField="FecPed" DataFormatString="{0:dd-MM-yyyy}"/>  
                                    <asp:BoundField HeaderText="Ven" DataField="ProPed"/>  
                                    <asp:BoundField HeaderText="VenName" DataField="NomPro"/>
                                    <asp:BoundField HeaderText="DeliveryDate" DataField="PlaPie" DataFormatString="{0:dd-MM-yyyy}"/>                                       
                                    <asp:BoundField HeaderText="Price" DataField="PrePie"/>
                                    <asp:BoundField HeaderText="OrdQty" DataField="PiePed" />
                                    <asp:BoundField HeaderText="Recvd" DataField="PieRec" />
                                     <asp:TemplateField HeaderText="Pending" ControlStyle-Width="3%">    
                                        <ItemTemplate>
                                            <asp:Literal ID="Literal4" runat="server" Text='<%# (System.Convert.ToInt64(Eval("PiePed"))-System.Convert.ToInt64(Eval("PieRec"))) %>'></asp:Literal>
                                            <%--<asp:Literal ID="Literal4" runat="server" Text='<%# (Eval("HorFin")!=null && Eval("HorFin")!=null)? (System.Convert.ToDateTime(Eval("Horfin"))-System.Convert.ToDateTime(Eval("HorIni"))).ToString():"00.00.00" %>'></asp:Literal> --%>
                                        </ItemTemplate>
                                        <ControlStyle Width="3%" />
                                        </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="Red" BorderColor="Black" Font-Size="12px" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" 
                                Font-Size="13px" BackColor="White"/>

                    </asp:GridView>

                    </center>
                </asp:Panel>
                <div style="float:right; width:48%; margin:auto; margin-top:20px;">
                    <asp:Button ID="seeocbtn" runat="server" Text="OC" onclick="seeocbtn_click" style="visibility:hidden;"/>
                    <asp:Button ID="PcPbBtn" runat="server" Font-Bold="True" Text="PC" 
                        onclick="PcPbBtn_Click"/>
                    
                    <asp:Button ID="download" runat="server" Text="Download" 
                        onclick="download_click" Font-Bold="True"/>
                    <asp:Button ID="openInNewBtn" runat="server" Text="Open In New Tab" onclick="newTab_click" Font-Bold="True"/>
                    <asp:Button ID="pdwgbtn" runat="server" Text="P_DWG" Font-Bold="True" OnClick="btnP_DWG_Click"/>
                </div>

                   <iframe id="myframe" runat="server" style="width:48%; margin:auto; float:right; margin-top:10px; height:569px;">
                       
                   </iframe>
                             
              </asp:Panel>
        </div>
    <asp:Label ID="checkpathpdf" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="randomlbl" runat="server" Visible="false"></asp:Label>

    </form>

    <script>
        document.onkeyup = function (e) {
            if (e.altKey && e.which == 65) {
                document.getElementById("seeocbtn").style.visibility = "visible";
            }
        };
    </script>
</body>
</html>
