Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports VB = Microsoft.VisualBasic
Imports System.IO
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Configuration
Imports System.Xml

Public Class test
    Inherits System.Web.UI.Page
    Private DBcmd As SqlCommand
    Private SQLstr As String = ""
    'Declare variabel for database configuration
    Dim DBstr As String = WebConfigurationManager.ConnectionStrings("BPF_Connect").ConnectionString
    Dim DBcon As New SqlConnection(DBstr)
    Private DRtbl As SqlDataReader
    Private DStbl As DataSet
    Private DVtbl As DataView
    Private DTtbl As DataTable
    Private DAtbl As SqlDataAdapter
    Dim LokasiDB As String
    Dim Conn As SqlConnection

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Me.BindGrid()
        End If
    End Sub
    Private Sub BindGrid()
        'Dim constr As String = ConfigurationManager.ConnectionStrings("Coba").ConnectionString
        'Using con As New SqlConnection(constr)
        '    Using cmd As New SqlCommand("SELECT AreaID, AreaFullName, AreaManager, 'true' AS Active FROM Area")
        '        Using sda As New SqlDataAdapter()
        '            cmd.Connection = con
        '            sda.SelectCommand = cmd
        '            Using dt As New DataTable()
        '                sda.Fill(dt)
        '                GridView1.DataSource = dt
        '                GridView1.DataBind()
        '            End Using
        '        End Using
        '    End Using
        'End Using
    End Sub
    Sub Koneksi()
        LokasiDB = "data source=10.168.2.243;initial catalog=BPF;Network Library=DBMSSOCN;User ID=rkm;Password=Rajawal1db"
        'LokasiDB = "data source=HERU-DEV\SQLSVR2012;initial catalog=BPF;Network Library=DBMSSOCN;User ID=sa;Password=bpfi"
        'LokasiDB = "data source=HERU-DEV\SQLSVR2012; Database=BPF; User ID=sa; Password=bpfi; Connection Timeout=0; Persist Security Info=true"
        Conn = New SqlConnection(LokasiDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    Protected Sub BtnArea_Click(sender As Object, e As EventArgs)
        Dim AreaId As String
        Dim AreaName As String
        Dim areaManager As String
        Dim active As Boolean
        Dim strLog As String

        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand("SELECT AreaID, AreaFullName, AreaManager, 'true' AS Active FROM Area", Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    AreaId = readerObj("AreaID").ToString
                    AreaName = readerObj("AreaFullName").ToString
                    areaManager = readerObj("AreaManager").ToString
                    active = readerObj("Active").ToString

                    '  For i = 1 To 10
                    'AreaId = "AreaId - " & i
                    'AreaName = "AreaName -" & i

                    'If i = 3 Then
                    '  AreaId = "AreaId - " & 2
                    'AreaName = "AreaName - " & 2
                    ' End If

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode

                    'xnode = bpfWS.syncArea(keypass, AreaId, AreaName, areaManager, active)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()

                    If isimsg.InnerText <> "" Then
                        lblArea.Text = isimsg.InnerText
                        'BindGrid()
                        lblArea.Text = "Insert" + AreaId + " - " & AreaName & " Successfully"
                        'lblArea.Text = "Insert" + i + " Successfully"
                        ' lblArea.Text = "Insert" & i & " Successfully"
                        Dim writer As System.IO.StreamWriter
                        Try
                            strLog = AreaId & ", " & AreaName & ", " & areaManager & ", " & active
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFAreaDebug_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & strLog & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                'BindGrid()
                                lblArea.Text = AreaId + " - " & AreaName & " is Exist"
                                ' lblArea.Text = i + " is Exist"
                                '    lblArea.Text = i & " is Exist"
                                ' lblArea.Text = isimsg.InnerText
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try
                    Else
                        'BindGrid()
                        lblArea.Text = "Insert Data Failed"
                    End If
                    ' Next
                End While
            End Using
            Conn.Close()
        End Using

    End Sub
    Protected Sub BtnBranch_Click(sender As Object, e As EventArgs)
        Dim branchCode As String
        Dim branchName As String
        Dim areaId As String
        Dim branchManager As String
        Dim alamat As String
        Dim active As Boolean
        Dim branchEmail As String
        Dim adminEmail As String
        Dim kota As String
        Dim surveyFee As Double
        Dim isDemo As Boolean
        Dim notelp As String
        Dim strsql As String

        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Koneksi()

        strsql = "SELECT BranchID AS branchcode, branchaddress as Alamat, BranchAreaPhone1+BranchPhone1 AS notelp, BranchFullName branchname, areaid, branchmanagername AS BranchManager, '' branchemail, BranchCity kota, 1 active, '' adminemail,0 SurveyFee,'1' isDemo FROM dbo.Branch"
        'Using cmdObj As New SqlClient.SqlCommand("SELECT BranchID AS branchCode, BranchFullName branchName, areaId, branchmanagername AS branchManager, 1 active, '' branchEmail, '' adminEmail FROM dbo.Branch
        '", Conn)
        Using cmdObj As New SqlClient.SqlCommand(strsql, Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    branchCode = readerObj("branchCode").ToString
                    branchName = readerObj("branchName").ToString
                    areaId = readerObj("areaId").ToString
                    branchManager = readerObj("branchManager").ToString
                    active = readerObj("active").ToString
                    branchEmail = readerObj("branchEmail").ToString
                    adminEmail = readerObj("adminEmail").ToString
                    kota = readerObj("kota").ToString
                    surveyFee = readerObj("surveyFee")
                    isDemo = readerObj("isDemo").ToString
                    alamat = readerObj("alamat").ToString
                    notelp = readerObj("notelp").ToString

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode

                    'xnode = bpfWS.syncArea(keypass, branchCode, branchName, areaId, branchManager, active, branchEmail, adminEmail)
                    'xnode = bpfWS.syncBranch(keypass, branchCode, branchName, alamat, notelp, areaId, branchManager, branchEmail, kota, active, adminEmail, surveyFee, isDemo)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



                    If isimsg.InnerText <> "" Then
                        Dim writer As System.IO.StreamWriter
                        Try
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFBranchDebug_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Branch : " & branchCode & "-" & branchName & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try

                    End If
                End While
            End Using
            Conn.Close()
        End Using





        'SQLstr = "Select * from Area WITH (NOLOCK) "
        ''Set command
        'DBcmd = New SqlCommand(SQLstr, DBcon)
        'DBcmd.CommandType = CommandType.Text
        ''Set parameter
        ''DBcmd.Parameters.AddWithValue("@Keypass_Pid", VB.Trim(Session("Keypass_Pid")))
        ''Execute reader
        'If (DBcon.State <> ConnectionState.Closed) Then : DBcon.Close() : End If
        'DBcon.Open()


        'DRtbl = DBcmd.ExecuteReader
        'If DRtbl.HasRows = True Then

        '    While DRtbl.Read()

        '        AreaId = DRtbl("AreaId")
        '    AreaName = DRtbl("AreaName")
        '    Dim bpfWS As New WS1.BPF_WS

        '    Dim xnode As XmlNode

        '    xnode = bpfWS.syncArea(keypass, AreaId, AreaName)

        '    Dim msgxml As String = "Msg"
        '    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()


        '    If isimsg.InnerText <> "" Then
        '        Dim writer As System.IO.StreamWriter
        '        Try
        '            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & AreaId & "   " & isimsg.InnerText
        '            If Not File.Exists(sfullname) Then
        '                writer = File.CreateText(sfullname)
        '                writer.Close()
        '            End If
        '            Using sw As StreamWriter = File.AppendText(sfullname)
        '                sw.WriteLine(merror)
        '                sw.Flush()
        '                sw.Close()
        '            End Using
        '        Catch
        '        Finally
        '        End Try

        '        End If


        '    End While

        '    DRtbl.Close()
        '    DBcmd.Dispose()
        '    DBcon.Close()

        'End If
    End Sub

    Protected Sub BtnCMO_Click(sender As Object, e As EventArgs)
        Dim cmoCode As String
        Dim cmoName As String
        Dim cmoEmail As String
        Dim branchCode As String
        Dim branchName As String
        Dim areaId As String
        Dim branchManager As String
        Dim branchEmail As String
        Dim adminEmail As String
        Dim branchActive As Boolean
        Dim cmoActive As Boolean
        Dim telp As String

        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand("SELECT TOP 10 rtrim(EmployeeID) AS CMOCode, EmployeeName AS CMOName, EMail AS CMOEmail, be.BranchID AS Branchcode, b.BranchFullName AS BranchName, a.AreaID, b.BranchManagerName AS Branchmanager,'' branchemail, '' adminemail, 1 branchactive, be.IsActive cmoactive,be.EmployeeAreaPhone1+be.EmployeePhone1 AS telp FROM dbo.BranchEmployee be INNER JOIN dbo.Branch b ON b.BranchID = be.BranchID INNER JOIN dbo.Area a ON a.AreaID = b.AreaID WHERE EmployeePosition='AO' and email<>'-'
", Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    cmoCode = readerObj("cmoCode").ToString
                    cmoName = readerObj("cmoName").ToString
                    cmoEmail = readerObj("cmoEmail").ToString
                    branchCode = readerObj("branchCode").ToString
                    branchName = readerObj("branchName").ToString
                    areaId = readerObj("areaId").ToString
                    branchManager = readerObj("branchManager").ToString
                    branchEmail = readerObj("branchEmail").ToString
                    adminEmail = readerObj("adminEmail").ToString
                    branchActive = readerObj("branchActive").ToString
                    cmoActive = readerObj("cmoActive").ToString
                    telp = readerObj("telp").ToString

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode


                    'xnode = bpfWS.syncCMO(keypass, cmoCode, cmoName, cmoEmail, branchCode, branchName, areaId, branchManager, branchEmail, adminEmail, branchActive, cmoActive, telp)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



                    If isimsg.InnerText <> "" Then
                        Dim writer As System.IO.StreamWriter
                        Try
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFCMODebug_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " CMO : " & cmoCode & "-" & cmoName & ", " & branchName & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try

                    End If
                End While
            End Using
            Conn.Close()
        End Using





        'SQLstr = "Select * from Area WITH (NOLOCK) "
        ''Set command
        'DBcmd = New SqlCommand(SQLstr, DBcon)
        'DBcmd.CommandType = CommandType.Text
        ''Set parameter
        ''DBcmd.Parameters.AddWithValue("@Keypass_Pid", VB.Trim(Session("Keypass_Pid")))
        ''Execute reader
        'If (DBcon.State <> ConnectionState.Closed) Then : DBcon.Close() : End If
        'DBcon.Open()


        'DRtbl = DBcmd.ExecuteReader
        'If DRtbl.HasRows = True Then

        '    While DRtbl.Read()

        '        AreaId = DRtbl("AreaId")
        '    AreaName = DRtbl("AreaName")
        '    Dim bpfWS As New WS1.BPF_WS

        '    Dim xnode As XmlNode

        '    xnode = bpfWS.syncArea(keypass, AreaId, AreaName)

        '    Dim msgxml As String = "Msg"
        '    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()


        '    If isimsg.InnerText <> "" Then
        '        Dim writer As System.IO.StreamWriter
        '        Try
        '            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & AreaId & "   " & isimsg.InnerText
        '            If Not File.Exists(sfullname) Then
        '                writer = File.CreateText(sfullname)
        '                writer.Close()
        '            End If
        '            Using sw As StreamWriter = File.AppendText(sfullname)
        '                sw.WriteLine(merror)
        '                sw.Flush()
        '                sw.Close()
        '            End Using
        '        Catch
        '        Finally
        '        End Try

        '        End If


        '    End While

        '    DRtbl.Close()
        '    DBcmd.Dispose()
        '    DBcon.Close()

        'End If
    End Sub

    Protected Sub BtnCarBranch_Click(sender As Object, e As EventArgs)
        Dim carOriginAssetCode As String
        Dim carBrandName As String
        Dim carBrandActive As Boolean
        Dim carBrandAssetCode As String

        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand(";With merk As (Select Description carBrandName, IsActive As carBranchActive, AssetCode As carBrandAssetCode FROM dbo.AssetMaster WHERE AssetLevel=1),
model As (SELECT am.assetcode AS assetcode2, carBrandAssetCode assetcode1 FROM dbo.AssetMaster am
  types AS (SELECT amt.ManufacturerCountry, amt.AssetCode As AssetCode3, model.assetcode2, model.assetcode1  FROM dbo.AssetMaster amt INNER JOIN model ON amt.AssetParent=model.assetcode2)
Select Case types.ManufacturerCountry As carOriginAssetCode,merk.* FROM merk INNER JOIN types On merk.carBrandAssetCode=types.assetcode1", Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    carOriginAssetCode = readerObj("carOriginAssetCode").ToString
                    carBrandName = readerObj("carBrandName").ToString
                    carBrandActive = readerObj("carBrandActive").ToString
                    carBrandAssetCode = readerObj("carBrandAssetCode").ToString

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode

                    'xnode = bpfWS.syncArea(keypass, carOriginAssetCode, carBrandName, carBrandActive, carBrandAssetCode)


                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



                    If isimsg.InnerText <> "" Then
                        Dim writer As System.IO.StreamWriter
                        Try
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & carBrandAssetCode & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try

                    End If
                End While
            End Using
            Conn.Close()
        End Using





        'SQLstr = "Select * from Area WITH (NOLOCK) "
        ''Set command
        'DBcmd = New SqlCommand(SQLstr, DBcon)
        'DBcmd.CommandType = CommandType.Text
        ''Set parameter
        ''DBcmd.Parameters.AddWithValue("@Keypass_Pid", VB.Trim(Session("Keypass_Pid")))
        ''Execute reader
        'If (DBcon.State <> ConnectionState.Closed) Then : DBcon.Close() : End If
        'DBcon.Open()


        'DRtbl = DBcmd.ExecuteReader
        'If DRtbl.HasRows = True Then

        '    While DRtbl.Read()

        '        AreaId = DRtbl("AreaId")
        '    AreaName = DRtbl("AreaName")
        '    Dim bpfWS As New WS1.BPF_WS

        '    Dim xnode As XmlNode

        '    xnode = bpfWS.syncArea(keypass, AreaId, AreaName)

        '    Dim msgxml As String = "Msg"
        '    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()


        '    If isimsg.InnerText <> "" Then
        '        Dim writer As System.IO.StreamWriter
        '        Try
        '            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & AreaId & "   " & isimsg.InnerText
        '            If Not File.Exists(sfullname) Then
        '                writer = File.CreateText(sfullname)
        '                writer.Close()
        '            End If
        '            Using sw As StreamWriter = File.AppendText(sfullname)
        '                sw.WriteLine(merror)
        '                sw.Flush()
        '                sw.Close()
        '            End Using
        '        Catch
        '        Finally
        '        End Try

        '        End If


        '    End While

        '    DRtbl.Close()
        '    DBcmd.Dispose()
        '    DBcon.Close()

        'End If
    End Sub

    Protected Sub BtnCarModel_Click(sender As Object, e As EventArgs)
        Dim carBrandAssetCode As String
        Dim carModelAssetCode As String
        Dim carModelName As String
        Dim carModelActive As Boolean

        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand("SELECT AssetParent AS carBrandAssetCode, AssetCode AS carModelAssetCode,Description AS carModelName,IsActive AS carModelActive FROM dbo.AssetMaster WHERE AssetLevel=2
", Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    carBrandAssetCode = readerObj("carBrandAssetCode").ToString
                    carModelAssetCode = readerObj("carModelAssetCode").ToString
                    carModelName = readerObj("carModelName").ToString
                    carModelActive = readerObj("carModelActive").ToString

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode

                    'xnode = bpfWS.syncArea(keypass, carBrandAssetCode, carModelAssetCode, carModelName, carModelActive)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



                    If isimsg.InnerText <> "" Then
                        Dim writer As System.IO.StreamWriter
                        Try
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & carBrandAssetCode & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try

                    End If
                End While
            End Using
            Conn.Close()
        End Using





        'SQLstr = "Select * from Area WITH (NOLOCK) "
        ''Set command
        'DBcmd = New SqlCommand(SQLstr, DBcon)
        'DBcmd.CommandType = CommandType.Text
        ''Set parameter
        ''DBcmd.Parameters.AddWithValue("@Keypass_Pid", VB.Trim(Session("Keypass_Pid")))
        ''Execute reader
        'If (DBcon.State <> ConnectionState.Closed) Then : DBcon.Close() : End If
        'DBcon.Open()


        'DRtbl = DBcmd.ExecuteReader
        'If DRtbl.HasRows = True Then

        '    While DRtbl.Read()

        '        AreaId = DRtbl("AreaId")
        '    AreaName = DRtbl("AreaName")
        '    Dim bpfWS As New WS1.BPF_WS

        '    Dim xnode As XmlNode

        '    xnode = bpfWS.syncArea(keypass, AreaId, AreaName)

        '    Dim msgxml As String = "Msg"
        '    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()


        '    If isimsg.InnerText <> "" Then
        '        Dim writer As System.IO.StreamWriter
        '        Try
        '            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & AreaId & "   " & isimsg.InnerText
        '            If Not File.Exists(sfullname) Then
        '                writer = File.CreateText(sfullname)
        '                writer.Close()
        '            End If
        '            Using sw As StreamWriter = File.AppendText(sfullname)
        '                sw.WriteLine(merror)
        '                sw.Flush()
        '                sw.Close()
        '            End Using
        '        Catch
        '        Finally
        '        End Try

        '        End If


        '    End While

        '    DRtbl.Close()
        '    DBcmd.Dispose()
        '    DBcon.Close()

        'End If
    End Sub

    Protected Sub BtnCarModelType_Click(sender As Object, e As EventArgs)
        'Dim carModelAssetCode As String
        'Dim carTypeAssetCode As String
        'Dim carModelTypeAssetCode As String
        'Dim carModelTypeName As String
        'Dim carModelTypeActive As Boolean

        'Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        'Koneksi()
        'Using cmdObj As New SqlClient.SqlCommand("SELECT AreaID, AreaFullName, AreaManager, 'true' AS Active FROM Area", Conn)
        '    Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
        '        While readerObj.Read
        '            carModelAssetCode = readerObj("carModelAssetCode").ToString
        '            carTypeAssetCode = readerObj("carTypeAssetCode").ToString
        '            carModelTypeAssetCode = readerObj("carModelTypeAssetCode").ToString
        '            carModelTypeName = readerObj("carModelTypeName").ToString
        '            carModelTypeActive = readerObj("carModelTypeActive").ToString

        '            Dim bpfWS As New WS1.BPF_WS

        '            Dim xnode As XmlNode

        '            xnode = bpfWS.syncArea(keypass, carModelAssetCode, carTypeAssetCode, carModelTypeAssetCode, carModelTypeName, carModelTypeActive)

        '            Dim msgxml As String = "Msg"
        '            Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '            isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



        '            If isimsg.InnerText <> "" Then
        '                Dim writer As System.IO.StreamWriter
        '                Try
        '                    Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '                    Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '                    Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '                    Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & carModelAssetCode & "   " & isimsg.InnerText
        '                    If Not File.Exists(sfullname) Then
        '                        writer = File.CreateText(sfullname)
        '                        writer.Close()
        '                    End If
        '                    Using sw As StreamWriter = File.AppendText(sfullname)
        '                        sw.WriteLine(merror)
        '                        sw.Flush()
        '                        sw.Close()
        '                    End Using
        '                Catch
        '                Finally
        '                End Try

        '            End If
        '        End While
        '    End Using
        '    Conn.Close()
        'End Using





        'SQLstr = "Select * from Area WITH (NOLOCK) "
        ''Set command
        'DBcmd = New SqlCommand(SQLstr, DBcon)
        'DBcmd.CommandType = CommandType.Text
        ''Set parameter
        ''DBcmd.Parameters.AddWithValue("@Keypass_Pid", VB.Trim(Session("Keypass_Pid")))
        ''Execute reader
        'If (DBcon.State <> ConnectionState.Closed) Then : DBcon.Close() : End If
        'DBcon.Open()


        'DRtbl = DBcmd.ExecuteReader
        'If DRtbl.HasRows = True Then

        '    While DRtbl.Read()

        '        AreaId = DRtbl("AreaId")
        '    AreaName = DRtbl("AreaName")
        '    Dim bpfWS As New WS1.BPF_WS

        '    Dim xnode As XmlNode

        '    xnode = bpfWS.syncArea(keypass, AreaId, AreaName)

        '    Dim msgxml As String = "Msg"
        '    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()


        '    If isimsg.InnerText <> "" Then
        '        Dim writer As System.IO.StreamWriter
        '        Try
        '            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & AreaId & "   " & isimsg.InnerText
        '            If Not File.Exists(sfullname) Then
        '                writer = File.CreateText(sfullname)
        '                writer.Close()
        '            End If
        '            Using sw As StreamWriter = File.AppendText(sfullname)
        '                sw.WriteLine(merror)
        '                sw.Flush()
        '                sw.Close()
        '            End Using
        '        Catch
        '        Finally
        '        End Try

        '        End If


        '    End While

        '    DRtbl.Close()
        '    DBcmd.Dispose()
        '    DBcon.Close()

        'End If
    End Sub

    Protected Sub BtnCarOrigin_Click(sender As Object, e As EventArgs)
        'Dim carOriginName As String
        'Dim carOriginAssetCode As String
        'Dim carOriginActive As Boolean

        'Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        'Koneksi()
        'Using cmdObj As New SqlClient.SqlCommand("SELECT AreaID, AreaFullName, AreaManager, 'true' AS Active FROM Area", Conn)
        '    Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
        '        While readerObj.Read
        '            carOriginName = readerObj("carOriginName").ToString
        '            carOriginAssetCode = readerObj("carOriginAssetCode").ToString
        '            carOriginActive = readerObj("carOriginActive").ToString

        '            Dim bpfWS As New WS1.BPF_WS

        '            Dim xnode As XmlNode

        '            xnode = bpfWS.syncArea(keypass, carOriginName, carOriginAssetCode, carOriginActive)

        '            Dim msgxml As String = "Msg"
        '            Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '            isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



        '            If isimsg.InnerText <> "" Then
        '                Dim writer As System.IO.StreamWriter
        '                Try
        '                    Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '                    Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '                    Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '                    Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & carOriginName & "   " & isimsg.InnerText
        '                    If Not File.Exists(sfullname) Then
        '                        writer = File.CreateText(sfullname)
        '                        writer.Close()
        '                    End If
        '                    Using sw As StreamWriter = File.AppendText(sfullname)
        '                        sw.WriteLine(merror)
        '                        sw.Flush()
        '                        sw.Close()
        '                    End Using
        '                Catch
        '                Finally
        '                End Try

        '            End If
        '        End While
        '    End Using
        '    Conn.Close()
        'End Using





        'SQLstr = "Select * from Area WITH (NOLOCK) "
        ''Set command
        'DBcmd = New SqlCommand(SQLstr, DBcon)
        'DBcmd.CommandType = CommandType.Text
        ''Set parameter
        ''DBcmd.Parameters.AddWithValue("@Keypass_Pid", VB.Trim(Session("Keypass_Pid")))
        ''Execute reader
        'If (DBcon.State <> ConnectionState.Closed) Then : DBcon.Close() : End If
        'DBcon.Open()


        'DRtbl = DBcmd.ExecuteReader
        'If DRtbl.HasRows = True Then

        '    While DRtbl.Read()

        '        AreaId = DRtbl("AreaId")
        '    AreaName = DRtbl("AreaName")
        '    Dim bpfWS As New WS1.BPF_WS

        '    Dim xnode As XmlNode

        '    xnode = bpfWS.syncArea(keypass, AreaId, AreaName)

        '    Dim msgxml As String = "Msg"
        '    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()


        '    If isimsg.InnerText <> "" Then
        '        Dim writer As System.IO.StreamWriter
        '        Try
        '            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & AreaId & "   " & isimsg.InnerText
        '            If Not File.Exists(sfullname) Then
        '                writer = File.CreateText(sfullname)
        '                writer.Close()
        '            End If
        '            Using sw As StreamWriter = File.AppendText(sfullname)
        '                sw.WriteLine(merror)
        '                sw.Flush()
        '                sw.Close()
        '            End Using
        '        Catch
        '        Finally
        '        End Try

        '        End If


        '    End While

        '    DRtbl.Close()
        '    DBcmd.Dispose()
        '    DBcon.Close()

        'End If
    End Sub

    Protected Sub BtnCarType_Click(sender As Object, e As EventArgs)
        Dim carTypeName As String
        Dim carTypeActive As Boolean
        Dim allowNoInsurance As String
        Dim carTypeAssetCode As String


        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand("SELECT Description AS carTypeName,IsActive ascarTypeActive, '' allowNoInsurance, AssetCode carTypeAssetCode FROM dbo.AssetMaster WHERE AssetLevel=3
", Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    carTypeName = readerObj("carTypeName").ToString
                    carTypeActive = readerObj("carTypeActive").ToString
                    allowNoInsurance = readerObj("allowNoInsurance").ToString
                    carTypeAssetCode = readerObj("carTypeAssetCode").ToString

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode

                    'xnode = bpfWS.syncArea(keypass, carTypeName, carTypeActive, allowNoInsurance, carTypeAssetCode)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



                    If isimsg.InnerText <> "" Then
                        Dim writer As System.IO.StreamWriter
                        Try
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & carTypeName & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try

                    End If
                End While
            End Using
            Conn.Close()
        End Using





        'SQLstr = "Select * from Area WITH (NOLOCK) "
        ''Set command
        'DBcmd = New SqlCommand(SQLstr, DBcon)
        'DBcmd.CommandType = CommandType.Text
        ''Set parameter
        ''DBcmd.Parameters.AddWithValue("@Keypass_Pid", VB.Trim(Session("Keypass_Pid")))
        ''Execute reader
        'If (DBcon.State <> ConnectionState.Closed) Then : DBcon.Close() : End If
        'DBcon.Open()


        'DRtbl = DBcmd.ExecuteReader
        'If DRtbl.HasRows = True Then

        '    While DRtbl.Read()

        '        AreaId = DRtbl("AreaId")
        '    AreaName = DRtbl("AreaName")
        '    Dim bpfWS As New WS1.BPF_WS

        '    Dim xnode As XmlNode

        '    xnode = bpfWS.syncArea(keypass, AreaId, AreaName)

        '    Dim msgxml As String = "Msg"
        '    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
        '    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()


        '    If isimsg.InnerText <> "" Then
        '        Dim writer As System.IO.StreamWriter
        '        Try
        '            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
        '            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
        '            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
        '            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & AreaId & "   " & isimsg.InnerText
        '            If Not File.Exists(sfullname) Then
        '                writer = File.CreateText(sfullname)
        '                writer.Close()
        '            End If
        '            Using sw As StreamWriter = File.AppendText(sfullname)
        '                sw.WriteLine(merror)
        '                sw.Flush()
        '                sw.Close()
        '            End Using
        '        Catch
        '        Finally
        '        End Try

        '        End If


        '    End While

        '    DRtbl.Close()
        '    DBcmd.Dispose()
        '    DBcon.Close()

        'End If
    End Sub

    Protected Sub BtnDealer_Click(sender As Object, e As EventArgs)
        Dim dealerCode As String
        Dim dealerName As String
        Dim dealerEmail As String
        Dim dealerActive As Boolean

        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand("SELECT SupplierID AS dealerCode, SupplierName AS Dealername,'' dealerEmail, IsActive AS dealerActive  FROM dbo.Supplier
", Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    dealerCode = readerObj("dealerCode").ToString
                    dealerName = readerObj("dealerName").ToString
                    dealerEmail = readerObj("dealerEmail").ToString
                    dealerActive = readerObj("dealerActive").ToString

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode

                    'xnode = bpfWS.syncArea(keypass, dealerCode, dealerName, dealerEmail, dealerActive)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()



                    If isimsg.InnerText <> "" Then
                        Dim writer As System.IO.StreamWriter
                        Try
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "KreditBPFadminDebug_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & " Area ID : " & dealerCode & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try

                    End If
                End While
            End Using
            Conn.Close()
        End Using

    End Sub


    Protected Sub BtnsetBPF_Click(sender As Object, e As EventArgs)
        Dim tahun As String
        Dim bulan As String
        Dim areacode As String
        Dim branchcode As String
        Dim cmocode As String
        Dim Collected As String
        Dim OPEX As Double
        Dim AIDA As Double
        Dim PL As Double
        Dim OSAR As Double
        Dim OSAR60 As Double
        Dim InterestIncome As Double
        Dim sales As Double
        Dim strLog As String
        Dim strsql As String
        Dim dteawal As String
        Dim dteakhir As String
        Dim tglawal As DateTime
        Dim tglakhir As DateTime
        Dim tr_date As DateTime
        Dim dblOpexvsOSAR As Double
        Dim dblOsAR60vsOSAR As Double
        Dim dblincomeInterestvsOSAR As Double
        Dim dblopexvsOSAR_Area As Double
        Dim dblOsAR60vsOSAR_Area As Double
        Dim dblincomeInterestvsOSAR_Area As Double


        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        'strsql = "SELECT Year(hdr.Tr_Date) tahun, MONTH(hdr.Tr_Date) bulan, b.AreaID AS areaCode, dtl.CoaBranch AS branchCode,"
        'strsql = strsql & " SUM(CASE WHEN (dtl.CoaId like '11520100%' OR dtl.CoaId LIKE '11510100%') AND dtl.Post='D'  THEN dtl.Amount ELSE 0 END) Collected,"
        'strsql = strsql & " SUM(Case When dtl.CoaId Like '14010300%' THEN dtl.Amount ELSE 0 END) AIDA,"
        'strsql = strsql & " SUM(CASE WHEN dtl.CoaId LIKE '7%' THEN dtl.Amount ELSE 0 END) OPEX,"
        'strsql = strsql & " SUM(Case When dtl.CoaId Like '6%' or dtl.CoaId like '7%'  THEN dtl.Amount ELSE 0 END) PL"
        'strsql = strsql & " FROM dbo.GLJournalH hdr INNER JOIN dbo.GLJournalD dtl ON dtl.Tr_Nomor = hdr.Tr_Nomor INNER JOIN dbo.Branch b ON b.BranchID = dtl.CoaBranch"
        'strsql = strsql & " WHERE YEAR(hdr.Tr_Date)=2018 And MONTH(hdr.Tr_Date)=4 and b.branchid<>'073'"
        'strsql = strsql & " GROUP BY YEAR(hdr.Tr_Date), MONTH(hdr.Tr_Date),  b.AreaID, dtl.CoaBranch"
        'strsql = strsql & " order BY  MONTH(hdr.Tr_Date), dtl.CoaBranch"

        'dteawal = "2018-05-01"
        'dteakhir = "2018-05-04"
        tglawal = txtTglAwal.Text
        tglakhir = txtTglAkhir.Text

        dteawal = tglawal.ToString("yyyy-MM-dd")
        dteakhir = tglakhir.ToString("yyyy-MM-dd")

        'Dim sqlcomm As New SqlCommand
        'strsql = "exec DashboardHistory_Insert '" & dteawal & "', '" & dteakhir & "'"

        'Koneksi()
        'sqlcomm.Connection = Conn
        'sqlcomm.CommandText = strsql
        'sqlcomm.CommandType = CommandType.Text
        'sqlcomm.ExecuteNonQuery()

        'strsql = "exec dashboardbpf '" & dteawal & "', '" & dteakhir & "'"
        'strsql = "select * from dashboardhistory where tr_date='" & dteakhir & "'"


        strsql = ""
        strsql = "select * from dashboardhistory where tr_date>='" & dteawal & "' and tr_date<='" & dteakhir & "' order by tr_date"

        'Response.Write(dteawal.ToString("yyyyMMdd"))
        'Response.Write(dteakhir.ToString("yyyyMMdd"))

        'strsql = ";With tblbranch As (Select YEAR('" & dteakhir & "') tahun, MONTH('" & dteakhir & "') bulan , '" & dteakhir & "' as Tr_date, branchID, AreaID, BranchFullName FROM dbo.Branch WITH (NOLOCK) WHERE BranchID NOT IN ('900','073')), "
        'strsql = strsql + " sales As (Select YEAR(ag.GoLiveDate) tahun, MONTH(ag.GoLiveDate) bulan, '" & dteakhir & "' as Tr_date, Case When aa.AssetTypeID In ('HE','KPR') OR ag.BranchID='900' THEN '000' ELSE ag.BranchID END branchid, SUM(ag.NTF+ag.TotalBunga) sales "
        'strsql = strsql + " From dbo.Agreement ag WITH (NOLOCK) INNER Join dbo.AgreementAsset aa WITH (NOLOCK) ON aa.ApplicationID = ag.ApplicationID And aa.BranchID = ag.BranchID "
        'strsql = strsql + " Where CAST(GoLiveDate As Date) ='" & dteakhir & "'"
        'strsql = strsql + " Group By CASE WHEN aa.AssetTypeID IN ('HE','KPR') OR ag.BranchID='900' THEN '000' ELSE ag.BranchID END, Year(GoLiveDate), Month(GoLiveDate)), "
        'strsql = strsql + "  OSAR As (Select YEAR('" & dteakhir & "') tahun, MONTH('" & dteakhir & "') bulan, '" & dteakhir & "' as Tr_date, Case When aa.AssetTypeID In ('HE','KPR') OR da.BranchID='900' THEN '000' ELSE da.BranchID END branchid, "
        'strsql = strsql + " 			SUM(dbo.fnGetOutstandingPrincipalbyPostingDate(da.BranchID, da.ApplicationID,'" & dteakhir & "')) OSAR "
        'strsql = strsql + " 			From dbo.DailyAging da With (NOLOCK) INNER Join dbo.Agreement a With (NOLOCK) On a.ApplicationID = da.ApplicationID And a.BranchID = da.BranchID "
        'strsql = strsql + " 						INNER Join dbo.AgreementAsset aa WITH (NOLOCK) ON aa.BranchID = da.BranchID And aa.ApplicationID = da.ApplicationID "
        'strsql = strsql + " 			Where da.ContractStatus In ('AKT','OSP') AND da.DefaultStatus='NM' AND "
        'strsql = strsql + " dbo.fnGetOutstandingPrincipalbyPostingDate(da.BranchID, da.ApplicationId,'" & dteakhir & "')>0  And da.AgingDate='" & dteakhir & "' "
        'strsql = strsql + "   			Group BY Case When aa.AssetTypeID In ('HE','KPR') OR da.BranchID='900' THEN '000' ELSE da.BranchID END), "
        'strsql = strsql + " OSAR60 As (Select YEAR('" & dteakhir & "') tahun, MONTH('" & dteakhir & "') bulan, '" & dteakhir & "' as Tr_date, CASE WHEN aa.AssetTypeID IN ('HE','KPR') OR da.BranchID='900' THEN '000' ELSE da.BranchID END branchid,"
        'strsql = strsql + " 			SUM(dbo.fnGetOutstandingPrincipalbyPostingDate(da.BranchID, da.ApplicationID,'" & dteakhir & "')) OSAR60 "
        'strsql = strsql + "   			From dbo.DailyAging da WITH (NOLOCK) INNER Join dbo.Agreement a WITH (NOLOCK) ON a.ApplicationID = da.ApplicationID And a.BranchID = da.BranchID"
        'strsql = strsql + " 						INNER Join dbo.AgreementAsset aa With (NOLOCK) On aa.BranchID = da.BranchID And aa.ApplicationID = da.ApplicationID"
        'strsql = strsql + "   			where da.AgingDate ='" & dteakhir & "' And da.ContractStatus In ('AKT','OSP') AND da.DefaultStatus='NM' AND "
        'strsql = strsql + "   			dbo.fnGetOutstandingPrincipalbyPostingDate(da.BranchID, da.ApplicationId,'" & dteakhir & "')>0 And "
        'strsql = strsql + "   			Case When dbo.fnGetDataFromInstallmentSchedule('" & dteakhir & "',da.ApplicationID,'Due') <= 0 THEN 0"
        'strsql = strsql + "   			Else dbo.fnGetDataFromInstallmentSchedule('" & dteakhir & "',da.ApplicationID,'Due')+1  END > 60 "
        'strsql = strsql + "   			Group BY CASE WHEN aa.AssetTypeID IN ('HE','KPR') OR da.BranchID='900' THEN '000' ELSE da.BranchID END),	 "
        'strsql = strsql + " Collected As (Select Year(a.PostingDate) tahun, MONTH(a.PostingDate) bulan, '" & dteakhir & "' as Tr_date, Case When aa.AssetTypeID In ('HE','KPR') OR a.BranchID='900' THEN '000' ELSE a.BranchID END branchid, "
        'strsql = strsql + " 				SUM(a.PaidAmount) As collected, SUM(a.InterestAmount) As InterestIncome "
        'strsql = strsql + "   				From dbo.InstallmentSchedule a With (NOLOCK) INNER Join dbo.AgreementAsset aa With (NOLOCK) On aa.BranchID = a.BranchID And aa.ApplicationID = a.ApplicationID"
        'strsql = strsql + " 				 Where a.PostingDate ='" & dteakhir & "'"
        'strsql = strsql + "   				Group By Year(a.PostingDate), Month(a.PostingDate), Case When aa.AssetTypeID In ('HE','KPR') OR a.BranchID='900' THEN '000' ELSE a.BranchID END), "
        'strsql = strsql + "  acc AS (SELECT Year(hdr.Tr_Date) tahun, MONTH(hdr.Tr_Date) bulan, '" & dteakhir & "' as Tr_date, b.AreaID AS areaCode, CASE WHEN dtl.CoaBranch='900' THEN '000' ELSE dtl.CoaBranch END AS branchid, "
        'strsql = strsql + " 			ABS(SUM(Case When dtl.CoaId Like '14010300%' AND dtl.Post='D' THEN dtl.Amount ELSE 0 END)-SUM(Case When dtl.CoaId Like '14010300%' AND dtl.Post='C' THEN dtl.Amount ELSE 0 END)) AIDA,"
        'strsql = strsql + "   			ABS(SUM(Case When dtl.CoaId Like '7%' AND dtl.Post='D' THEN dtl.Amount ELSE 0 END)-SUM(Case When dtl.CoaId Like '7%' AND dtl.Post='C' THEN dtl.Amount ELSE 0 END)) OPEX, "
        'strsql = strsql + "   			ABS(SUM(Case When (dtl.CoaId Like '6%' or dtl.CoaId like '7%') AND dtl.Post='D' THEN dtl.Amount ELSE 0 END)-SUM(Case When (dtl.CoaId Like '6%' or dtl.CoaId like '7%') AND dtl.Post='C' THEN dtl.Amount ELSE 0 END)) PL,"
        'strsql = strsql + " 			ABS(SUM(Case When dtl.CoaId Like '11530100%' AND dtl.Post='D' THEN dtl.Amount ELSE 0 END)-SUM(Case When dtl.CoaId Like '11530100%' AND dtl.Post='C' THEN dtl.Amount ELSE 0 END)) Pokok_Factoring,  "
        'strsql = strsql + " 			ABS(SUM(Case When dtl.CoaId Like '60210300%' AND dtl.Post='D' THEN dtl.Amount ELSE 0 END)-SUM(Case When dtl.CoaId Like '60210300%' AND dtl.Post='C' THEN dtl.Amount ELSE 0 END)) income_Factoring  "
        'strsql = strsql + "   		From dbo.GLJournalH hdr WITH (NOLOCK) INNER Join dbo.GLJournalD dtl WITH (NOLOCK) ON dtl.Tr_Nomor = hdr.Tr_Nomor INNER Join dbo.Branch b ON b.BranchID = dtl.CoaBranch "
        'strsql = strsql + " 		WHERE hdr.Tr_Date ='" & dteakhir & "'"
        'strsql = strsql + " 		Group BY YEAR(hdr.Tr_Date), MONTH(hdr.Tr_Date),  b.AreaID, dtl.CoaBranch),"
        'strsql = strsql + " saldoAwal_ayda as (SELECT BranchId, Year('" & dteakhir & "') tahun, Month('" & dteakhir & "') bulan,'" & dteakhir & "' as Tr_date, "
        'strsql = strsql + "                         Case MONTH('" & dteakhir & "') "
        'strsql = strsql + " 							WHEN 1 Then BegBal WHEN 2 Then Bal1 WHEN 3 Then Bal2 WHEN 4 Then Bal3"
        'strsql = strsql + " 							When 5 Then Bal4 When 6 Then Bal5 When 7 Then Bal6 When 8 Then Bal7 When 9 Then Bal8"
        'strsql = strsql + " 							WHEN 10 Then Bal9 WHEN 11 Then Bal10 WHEN 12 Then Bal11 END saldoAwal"
        'strsql = strsql + " 						From dbo.GLBalance With (NOLOCK) Where SUBSTRING(CoaId, 1, 8) = '14010300' "
        'strsql = strsql + "             And [Year] = Year('" & dteakhir & "')),	   "
        'strsql = strsql + " OSAR_Magna AS (SELECT branchid, '" & dteakhir & "' as Tr_date,  SUM(outstandingpokok) OSAR_MGN, SUM(CASE WHEN due>60 THEN outstandingpokok ELSE 0 END) OSAR60_MGN  FROM dbo.dailyaging_magna WHERE tr_date='" & dteakhir & "' GROUP BY branchid),"
        'strsql = strsql + " Ayda_Magna As (Select branchid,'" & dteakhir & "' as Tr_date,  SUM(ayda_amount) ayda_mgn FROM dbo.ayda_magna WHERE tr_date='" & dteakhir & "' GROUP BY branchid),"
        'strsql = strsql + " PL_Magna AS (SELECT branchid,  '" & dteakhir & "' as Tr_date,  PL AS PL_Mgn FROM dbo.pl_magna WHERE tr_date='" & dteakhir & "')"
        'strsql = strsql + "  Select a.tahun, a.bulan, '" & dteakhir & "' tr_date, a.AreaID areacode, a.BranchID As Branchcode, 0 cmocode, ISNULL(c.collected,0) collected, "
        'strsql = strsql + " ISNULL(acc.OPEX, 0) OPEX, sa_ayda.saldoAwal+ISNULL(acc.AIDA,0)+ISNULL(ayda_mgn,0) AIDA, ISNULL(acc.PL,0)+ISNULL(PL_MGN.pl,0) PL, ISNULL(ar.OSAR,0)+ISNULL(acc.Pokok_Factoring,0)+ISNULL(OS_MGN.OSAR_MGN,0) OSAR,"
        'strsql = strsql + "  ISNULL(ar60.OSAR60, 0)+ISNULL(OS_MGN.OSAR60_MGN,0) OSAR60, ISNULL(c.InterestIncome,0)+ISNULL(acc.income_Factoring,0) InterestIncome,ISNULL(s.sales,0) sales,"
        'strsql = strsql + "  ISNULL(acc.OPEX, 0)*360/(ISNULL(ar.OSAR,0)+ISNULL(acc.Pokok_Factoring,0)+ISNULL(OS_MGN.OSAR_MGN,0))  opexvsOSAR,"
        'strsql = strsql + "(ISNULL(ar60.OSAR60, 0) + ISNULL(OS_MGN.OSAR60_MGN, 0))  / (ISNULL(ar.OSAR, 0) + ISNULL(acc.Pokok_Factoring, 0) + ISNULL(OS_MGN.OSAR_MGN, 0)) osAR60vsOSAR,"
        'strsql = strsql + "  (ISNULL(c.InterestIncome,0)+ISNULL(acc.income_Factoring,0))*360/(ISNULL(ar.OSAR,0)+ISNULL(acc.Pokok_Factoring,0)+ISNULL(OS_MGN.OSAR_MGN,0)) interestIncomevsOSAR"
        'strsql = strsql + " From tblbranch a left Join sales s On s.BranchID = a.BranchID And s.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join osar ar On ar.BranchID = a.BranchID And ar.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join osar60 ar60 On ar60.BranchID = a.BranchID And ar60.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join collected c On c.BranchID = a.BranchID And c.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join acc On acc.BranchID = a.BranchID And acc.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join saldoAwal_ayda sa_ayda On sa_ayda.BranchId = a.BranchID And sa_ayda.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join OSAR_Magna OS_MGN ON OS_MGN.branchid = a.BranchID And OS_MGN.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join Ayda_Magna Ayda_MGN On Ayda_MGN.branchid = a.BranchID And Ayda_MGN.Tr_date = a.Tr_date"
        'strsql = strsql + "  Left Join dbo.pl_magna PL_MGN ON PL_MGN.branchid = a.BranchID And PL_MGN.Tr_date = a.Tr_date"



        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand(strsql, Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    tahun = readerObj("tahun").ToString
                    bulan = readerObj("bulan").ToString
                    areacode = readerObj("areaCode").ToString
                    branchcode = readerObj("branchid").ToString
                    cmocode = "0"
                    Collected = readerObj("collected")
                    OPEX = readerObj("OPEX")
                    AIDA = readerObj("AYDA")
                    PL = readerObj("PL")
                    OSAR = readerObj("OSAR")
                    OSAR60 = readerObj("OSAR60")
                    InterestIncome = readerObj("InterestIncome")
                    sales = readerObj("sales")
                    tr_date = readerObj("tr_date")
                    'tr_date = readerObj("tr_date")
                    dblOpexvsOSAR = readerObj("opexvsOSAR")
                    dblOsAR60vsOSAR = readerObj("osAR60vsOSAR")
                    dblincomeInterestvsOSAR = readerObj("interestIncomevsOSAR")
                    dblincomeInterestvsOSAR_Area = readerObj("interestIncomevsOSAR_Area")
                    dblopexvsOSAR_Area = readerObj("OpexvsOSAR_Area")
                    dblOsAR60vsOSAR_Area = readerObj("OSAr60vsosar_area")


                    Dim bpfWS As New WS1.BPF_WS

                    Dim XNode As XmlNode

                    'XNode = bpfWS.setBPF_Datasource(tr_date, tahun, bulan, areacode, branchcode, cmocode, Collected, OPEX, AIDA, PL, keypass, OSAR, OSAR60, InterestIncome, sales)
                    XNode = bpfWS.setBPF_Datasource(tr_date, areacode, branchcode, cmocode, Collected, OPEX, AIDA, PL, keypass, OSAR, OSAR60, InterestIncome, dblOpexvsOSAR, dblOsAR60vsOSAR, dblincomeInterestvsOSAR, sales, dblopexvsOSAR_Area, dblOsAR60vsOSAR_Area, dblincomeInterestvsOSAR_Area)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(XNode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()

                    If isimsg.InnerText <> "" Then
                        lblArea.Text = isimsg.InnerText
                        'BindGrid()
                        'lblArea.Text = "Insert" + AreaId + " - " & AreaName & " Successfully"
                        'lblArea.Text = "Insert" + i + " Successfully"
                        ' lblArea.Text = "Insert" & i & " Successfully"
                        Dim writer As System.IO.StreamWriter
                        Try
                            strLog = dteakhir & ", " & areacode & ", " & branchcode & ", Collected : " & Collected.ToString & ", OPEX : " & OPEX.ToString & ", AIDA : " & AIDA & "| P/L : " & PL.ToString & "| OpexvsOSAR : " & dblOpexvsOSAR.ToString & "| OsAR60vsOSAR : " & dblOsAR60vsOSAR.ToString
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "SetBPF_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & strLog & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                'BindGrid()
                                'lblArea.Text = AreaId + " - " & AreaName & " is Exist"
                                ' lblArea.Text = i + " is Exist"
                                '    lblArea.Text = i & " is Exist"
                                ' lblArea.Text = isimsg.InnerText
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try
                    Else
                        'BindGrid()
                        lblArea.Text = "Insert Data Failed"
                    End If
                    ' Next
                End While
            End Using
            Conn.Close()
        End Using

    End Sub

    Protected Sub BtnTarget_Click(sender As Object, e As EventArgs)
        Dim tahun As String
        Dim bulan As String
        Dim areacode As String
        Dim branchcode As String
        Dim cmocode As String
        Dim targetAmount As String
        Dim targetNumApp As Integer
        Dim targetnumApproveApp As Integer
        Dim strLog As String
        Dim strsql As String
        Dim tglakhir As Date
        Dim dteakhir As Int16

        Dim keypass As String = "NaAsTYMvZzNURPXO6m3RCNOROK7RMb5mG7vUk1p7hCG5JLieFlxJbg7ypG6ayndYNdGJA1RzP"

        Dim bpfWS2 As New WS1.BPF_WS

        tglakhir = txtTglAkhir.Text

        dteakhir = tglakhir.Year

        'Dim xnode2 As XmlNode
        'xnode2 = bpfWS2.removeAllTarget(dteakhir, keypass)


        strsql = "SELECT tb.Year tahun, tb.Month bulan, b.AreaID AS areaCode, b.BranchID AS branchCode, '0' cmoCode, tb.TargetSales AS targetamount, tb.targetunit AS targetNumApp"
        strsql = strsql & " FROM dbo.Branch b INNER JOIN dbo.TblTargetBranch tb ON tb.BranchID = b.BranchID"
        strsql = strsql & " WHERE tb.Year=" & dteakhir & ""

        Koneksi()
        Using cmdObj As New SqlClient.SqlCommand(strsql, Conn)
            Using readerObj As SqlClient.SqlDataReader = cmdObj.ExecuteReader
                While readerObj.Read
                    tahun = readerObj("tahun").ToString
                    bulan = readerObj("bulan").ToString
                    areacode = readerObj("areaCode").ToString
                    branchcode = readerObj("branchcode").ToString
                    cmocode = "0"
                    targetAmount = readerObj("targetAmount")
                    targetNumApp = readerObj("targetNumApp")
                    targetnumApproveApp = targetNumApp

                    Dim bpfWS As New WS1.BPF_WS

                    Dim xnode As XmlNode

                    xnode = bpfWS.setTarget(tahun, bulan, areacode, branchcode, cmocode, targetAmount, targetNumApp, targetnumApproveApp, keypass)

                    Dim msgxml As String = "Msg"
                    Dim isimsg As XmlElement = TryCast(xnode.SelectSingleNode(Convert.ToString("//") & msgxml), XmlElement)
                    isimsg.InnerText = isimsg.InnerText.ToString() '(Int32.Parse(url1.InnerText) + 1).ToString()

                    If isimsg.InnerText <> "" Then
                        lblArea.Text = isimsg.InnerText
                        'BindGrid()
                        'lblArea.Text = "Insert" + AreaId + " - " & AreaName & " Successfully"
                        'lblArea.Text = "Insert" + i + " Successfully"
                        ' lblArea.Text = "Insert" & i & " Successfully"
                        Dim writer As System.IO.StreamWriter
                        Try
                            strLog = tahun & "-" & bulan & ", " & areacode & ", " & branchcode & ", target Amount : " & targetAmount.ToString & ", Unit : " & targetNumApp.ToString
                            Dim mtoday As String = String.Format("{0:yyyyMMdd}", System.DateTime.Today)
                            Dim mtime As String = [String].Format("{0:HH:mm:ss}", System.DateTime.Now)
                            Dim sfullname As String = ConfigurationManager.AppSettings("logFolder") + "\\" + "TargetBPF_" + mtoday + ".log"
                            Dim merror As String = Convert.ToString(mtime & Convert.ToString(";")) & strLog & "   " & isimsg.InnerText
                            If Not File.Exists(sfullname) Then
                                'BindGrid()
                                'lblArea.Text = AreaId + " - " & AreaName & " is Exist"
                                ' lblArea.Text = i + " is Exist"
                                '    lblArea.Text = i & " is Exist"
                                ' lblArea.Text = isimsg.InnerText
                                writer = File.CreateText(sfullname)
                                writer.Close()
                            End If
                            Using sw As StreamWriter = File.AppendText(sfullname)
                                sw.WriteLine(merror)
                                sw.Flush()
                                sw.Close()
                            End Using
                        Catch
                        Finally
                        End Try
                    Else
                        'BindGrid()
                        lblArea.Text = "Insert Data Failed"
                    End If
                    ' Next
                End While
            End Using
            Conn.Close()
        End Using

    End Sub

End Class