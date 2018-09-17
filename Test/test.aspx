<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="test.aspx.vb" Inherits="Test.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="css/ui-lightness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type = "text/javascript">
    $(document).ready(function () {
        $(".Calender").dynDateTime({
            showsTime: true,
            ifFormat: "%Y/%m/%d",
            daFormat: "%M %p, %e %m,  %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
        $(".Calender2").dynDateTime({
            showsTime: true,
            ifFormat: "%Y/%m/%d",
            daFormat: "%M %p, %e %m,  %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
</script>
    <style>
        *{
padding:0px;
margin:0px;
}
        .btn1 {
            cursor:pointer;
            background-color:Highlight;
            border: none;
            border-radius:5px;
            color:white;
            font-family:fontsarif;
        }
        .btn1:hover{
            background-color: cadetblue;
        }
        .headerin {
            width:100%;
            background-color:aquamarine;
            height:80px;
            position:fixed;
        }
        .tbl {
            width:100%;
            
            margin-top:0.5em;
            margin-left:0.2em;
            margin-right:0.2em;
        }
        @font-face {
            font-family:afont;
            src:url('../Fonts/ethnocentric1.ttf');
        }
        @font-face {
            font-family:fontsarif;
            src:url('../Fonts/OpenSans-Regular.ttf');
        }
        .Sidebar1{
            width:20%;
            height:600px;
            background-color:coral;
            float:left;
        }
        .content1 {
            width:80%;
            height:600px;
            background-color: lightblue;
            float:right;
        }
        .AreaCss{
            color:red;
            font-family:fontsarif;
            margin-top:15em;
            position:absolute;
            margin-left:2em;
        }
        .AreaCss1{
            color:red;
            font-family:fontsarif;
            margin-top:17em;
            position:absolute;
            margin-left:2em;
        }
        .grid{
           position:absolute;
           margin-top:10em;
           margin-left:1em; 
        }
    </style>
</head>
<body>
    <!--Test-->
    <form id="form1" runat="server">
    <div class="headerin">        
        <table class="tbl">
          
            <tr>
                <td><asp:Button ID="BtnArea" runat="server" Text="Area" Width="140" Height="45" CssClass="btn1" OnClick="BtnArea_Click" /></td> 
                <td>  <asp:Button ID="BtnBranch" runat="server" Text="Branch" Width="140" Height="45" CssClass="btn1" OnClick="BtnBranch_Click" /><asp:Label ID="lblBranch" runat="server"></asp:Label></td>  
                 <td>
                    <asp:Button ID="BtnCMO" runat="server" Text="CMO" Width="140" Height="45" CssClass="btn1" OnClick="BtnCMO_Click" /><asp:Label ID="lblCMO" runat="server"></asp:Label>
                </td>   
                <td>
                    <asp:Button ID="BtnSetBPF" runat="server" Text="Set BPF" Width="140" Height="45" CssClass="btn1" OnClick="BtnsetBPF_Click" /><asp:Label ID="lblSetBPF" runat="server"></asp:Label>
                </td>    
                <td>
                    <asp:Button ID="BtnTarget" runat="server" Text="Target Sales" Width="140" Height="45" CssClass="btn1" OnClick="BtnTarget_Click" /><asp:Label ID="LblTarget" runat="server"></asp:Label>
                </td>    
                 <td> <asp:Button ID="BtnCarBranch" runat="server" Text="Car Branch" Width="140" Height="45" CssClass="btn1" OnClick="BtnCarBranch_Click" /><asp:Label ID="lblCarBranch" runat="server"></asp:Label></td>   
                <td>
                    <asp:Button ID="BtnCarModel" runat="server" Text="Car Model" Width="140" Height="45" CssClass="btn1" OnClick="BtnCarModel_Click" /><asp:Label ID="lblCarModel" runat="server"></asp:Label>
                </td>
                 <td><asp:Button ID="BtnCarModelType" runat="server" Text="Car Model Type" Width="140" Height="45" CssClass="btn1" OnClick="BtnCarModelType_Click" /><asp:Label ID="lblCarModelType" runat="server"></asp:Label></td>
                  <td><asp:Button ID="BtnCarOrigin" runat="server" Text="Car Origin" Width="140" Height="45" CssClass="btn1" OnClick="BtnCarOrigin_Click" /><asp:Label ID="lblCarOrigin" runat="server"></asp:Label></td>
                <td><asp:Button ID="BtnCarType" runat="server" Text="Car Type" Width="140" Height="45" CssClass="btn1" OnClick="BtnCarType_Click" /><asp:Label ID="lblCarType" runat="server"></asp:Label></td>
                 <td><asp:Button ID="BtnDealer" runat="server" Text="Dealer" Width="140" Height="45" CssClass="btn1" OnClick="BtnDealer_Click" /><asp:Label ID="lblDealer" runat="server"></asp:Label></td>
            </tr>
           
        </table>
    </div>
 <div class="Sidebar1">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
             <div>

               <!--  <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" BorderColor="white">
                    <Columns>
                                            </Columns>
                 </asp:GridView>-->
                 <div>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="Label1" Text ="Start Date" runat="server" />
                     <br />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTglAwal" runat="server" class ="Calender" style="cursor :not-allowed" />
                     <img src="calender.png" class="pointer" />
                 </div>
                 <div>
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="Label2" Text ="End Date" runat="server" />
                     <br />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTglAkhir" runat="server" class ="Calender2" style="cursor :not-allowed" />
                     <img src="calender.png" class="pointer" />
                 </div>                
             </div>
        </div>
        <div class="content1">
            <asp:Label ID="lblArea" runat="server" CssClass="AreaCss"></asp:Label>
             <asp:Label ID="lblArea1" runat="server" CssClass="AreaCss1"></asp:Label>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" CssClass="grid">
                <Columns>
                    <asp:BoundField ItemStyle-Width="150px" DataField="AreaID" HeaderText="Area ID" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="AreaFullName" HeaderText="Area Fullname" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="AreaManager" HeaderText="Area Manager" />
                    <asp:BoundField ItemStyle-Width="150px" DataField="Active" HeaderText="Active" />
                </Columns>
            </asp:GridView>
        </div>
        <div>

        </div>
    </form>
</body>
</html>
