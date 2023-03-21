<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRepairMgmt
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.sidePanel = New System.Windows.Forms.Panel()
        Me.dgvRepairSearchResults = New System.Windows.Forms.DataGridView()
        Me.colRepairRef = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colCustSurname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colAssetDetails = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRepairStatus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnNewRepair = New System.Windows.Forms.Button()
        Me.btnSearchRepairs = New System.Windows.Forms.Button()
        Me.tbSurname = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbRepairRef = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblUserPrompt = New System.Windows.Forms.Label()
        Me.lblCustomerNameAddress = New System.Windows.Forms.Label()
        Me.lblAssetDetails = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMobilePhn = New System.Windows.Forms.Label()
        Me.lblEmailAddress = New System.Windows.Forms.Label()
        Me.lblHomePhn = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblIntakeDate = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblRepairRef = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblCurrentRepairStatus = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblFaultDesc = New System.Windows.Forms.Label()
        Me.btnUpdateRepairStatus = New System.Windows.Forms.Button()
        Me.btnUpdateRepairQuote = New System.Windows.Forms.Button()
        Me.lblQuote = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dgvRepairRemarks = New System.Windows.Forms.DataGridView()
        Me.colEmployee = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colTimestamp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.colRemark = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCustomerCollection = New System.Windows.Forms.Button()
        Me.btnAddRepairRemark = New System.Windows.Forms.Button()
        Me.sidePanel.SuspendLayout()
        CType(Me.dgvRepairSearchResults, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvRepairRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sidePanel
        '
        Me.sidePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.sidePanel.Controls.Add(Me.dgvRepairSearchResults)
        Me.sidePanel.Controls.Add(Me.btnNewRepair)
        Me.sidePanel.Controls.Add(Me.btnSearchRepairs)
        Me.sidePanel.Controls.Add(Me.tbSurname)
        Me.sidePanel.Controls.Add(Me.Label2)
        Me.sidePanel.Controls.Add(Me.tbRepairRef)
        Me.sidePanel.Controls.Add(Me.Label1)
        Me.sidePanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.sidePanel.Location = New System.Drawing.Point(0, 0)
        Me.sidePanel.Name = "sidePanel"
        Me.sidePanel.Size = New System.Drawing.Size(350, 729)
        Me.sidePanel.TabIndex = 1
        '
        'dgvRepairSearchResults
        '
        Me.dgvRepairSearchResults.AllowUserToAddRows = False
        Me.dgvRepairSearchResults.AllowUserToDeleteRows = False
        Me.dgvRepairSearchResults.AllowUserToResizeColumns = False
        Me.dgvRepairSearchResults.AllowUserToResizeRows = False
        Me.dgvRepairSearchResults.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.dgvRepairSearchResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRepairSearchResults.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvRepairSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRepairSearchResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colRepairRef, Me.colCustSurname, Me.colAssetDetails, Me.colRepairStatus})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(55, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRepairSearchResults.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvRepairSearchResults.GridColor = System.Drawing.Color.White
        Me.dgvRepairSearchResults.Location = New System.Drawing.Point(6, 106)
        Me.dgvRepairSearchResults.Name = "dgvRepairSearchResults"
        Me.dgvRepairSearchResults.ReadOnly = True
        Me.dgvRepairSearchResults.RowHeadersVisible = False
        Me.dgvRepairSearchResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRepairSearchResults.Size = New System.Drawing.Size(338, 618)
        Me.dgvRepairSearchResults.TabIndex = 5
        '
        'colRepairRef
        '
        Me.colRepairRef.HeaderText = "Repair Ref."
        Me.colRepairRef.Name = "colRepairRef"
        Me.colRepairRef.ReadOnly = True
        Me.colRepairRef.Width = 50
        '
        'colCustSurname
        '
        Me.colCustSurname.HeaderText = "Surname"
        Me.colCustSurname.Name = "colCustSurname"
        Me.colCustSurname.ReadOnly = True
        Me.colCustSurname.Width = 95
        '
        'colAssetDetails
        '
        Me.colAssetDetails.HeaderText = "Asset"
        Me.colAssetDetails.Name = "colAssetDetails"
        Me.colAssetDetails.ReadOnly = True
        Me.colAssetDetails.Width = 90
        '
        'colRepairStatus
        '
        Me.colRepairStatus.HeaderText = "Current Status"
        Me.colRepairStatus.Name = "colRepairStatus"
        Me.colRepairStatus.ReadOnly = True
        '
        'btnNewRepair
        '
        Me.btnNewRepair.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.btnNewRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNewRepair.Location = New System.Drawing.Point(133, 69)
        Me.btnNewRepair.Name = "btnNewRepair"
        Me.btnNewRepair.Size = New System.Drawing.Size(93, 31)
        Me.btnNewRepair.TabIndex = 4
        Me.btnNewRepair.Text = "New"
        Me.btnNewRepair.UseVisualStyleBackColor = False
        '
        'btnSearchRepairs
        '
        Me.btnSearchRepairs.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.btnSearchRepairs.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchRepairs.Location = New System.Drawing.Point(251, 69)
        Me.btnSearchRepairs.Name = "btnSearchRepairs"
        Me.btnSearchRepairs.Size = New System.Drawing.Size(93, 31)
        Me.btnSearchRepairs.TabIndex = 3
        Me.btnSearchRepairs.Text = "Search"
        Me.btnSearchRepairs.UseVisualStyleBackColor = False
        '
        'tbSurname
        '
        Me.tbSurname.Location = New System.Drawing.Point(133, 36)
        Me.tbSurname.Name = "tbSurname"
        Me.tbSurname.Size = New System.Drawing.Size(212, 26)
        Me.tbSurname.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 18)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Surname:"
        '
        'tbRepairRef
        '
        Me.tbRepairRef.Location = New System.Drawing.Point(133, 5)
        Me.tbRepairRef.Name = "tbRepairRef"
        Me.tbRepairRef.Size = New System.Drawing.Size(212, 26)
        Me.tbRepairRef.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Repair Reference:"
        '
        'lblUserPrompt
        '
        Me.lblUserPrompt.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserPrompt.Location = New System.Drawing.Point(557, 22)
        Me.lblUserPrompt.Name = "lblUserPrompt"
        Me.lblUserPrompt.Size = New System.Drawing.Size(271, 79)
        Me.lblUserPrompt.TabIndex = 2
        Me.lblUserPrompt.Text = "Select a repair from the list on the left" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "to view its details here."
        Me.lblUserPrompt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCustomerNameAddress
        '
        Me.lblCustomerNameAddress.Font = New System.Drawing.Font("Tahoma", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerNameAddress.Location = New System.Drawing.Point(356, 9)
        Me.lblCustomerNameAddress.Name = "lblCustomerNameAddress"
        Me.lblCustomerNameAddress.Size = New System.Drawing.Size(318, 92)
        Me.lblCustomerNameAddress.TabIndex = 2
        Me.lblCustomerNameAddress.Text = "<title> <forename> <surname>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<address line 1>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<city>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<postcode>"
        '
        'lblAssetDetails
        '
        Me.lblAssetDetails.Font = New System.Drawing.Font("Tahoma", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAssetDetails.Location = New System.Drawing.Point(680, 9)
        Me.lblAssetDetails.Name = "lblAssetDetails"
        Me.lblAssetDetails.Size = New System.Drawing.Size(316, 92)
        Me.lblAssetDetails.TabIndex = 3
        Me.lblAssetDetails.Text = "<asset make> <asset model>" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "<serial number>"
        Me.lblAssetDetails.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(357, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(113, 18)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Mobile Phone:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(357, 146)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(105, 18)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Home Phone:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(605, 119)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(118, 18)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Email Address:"
        '
        'lblMobilePhn
        '
        Me.lblMobilePhn.Location = New System.Drawing.Point(476, 119)
        Me.lblMobilePhn.Name = "lblMobilePhn"
        Me.lblMobilePhn.Size = New System.Drawing.Size(123, 18)
        Me.lblMobilePhn.TabIndex = 7
        Me.lblMobilePhn.Text = "07123456789"
        Me.lblMobilePhn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEmailAddress
        '
        Me.lblEmailAddress.Location = New System.Drawing.Point(729, 119)
        Me.lblEmailAddress.Name = "lblEmailAddress"
        Me.lblEmailAddress.Size = New System.Drawing.Size(267, 18)
        Me.lblEmailAddress.TabIndex = 9
        Me.lblEmailAddress.Text = "someone@example.com"
        Me.lblEmailAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHomePhn
        '
        Me.lblHomePhn.Location = New System.Drawing.Point(476, 146)
        Me.lblHomePhn.Name = "lblHomePhn"
        Me.lblHomePhn.Size = New System.Drawing.Size(123, 18)
        Me.lblHomePhn.TabIndex = 10
        Me.lblHomePhn.Text = "01234567890"
        Me.lblHomePhn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(357, 199)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(155, 18)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Repair Intake Date:"
        '
        'lblIntakeDate
        '
        Me.lblIntakeDate.Location = New System.Drawing.Point(539, 199)
        Me.lblIntakeDate.Name = "lblIntakeDate"
        Me.lblIntakeDate.Size = New System.Drawing.Size(122, 18)
        Me.lblIntakeDate.TabIndex = 12
        Me.lblIntakeDate.Text = "dd/mm/yyyy"
        Me.lblIntakeDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(681, 199)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(207, 18)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Repair Reference Number:"
        '
        'lblRepairRef
        '
        Me.lblRepairRef.Location = New System.Drawing.Point(894, 199)
        Me.lblRepairRef.Name = "lblRepairRef"
        Me.lblRepairRef.Size = New System.Drawing.Size(102, 18)
        Me.lblRepairRef.TabIndex = 14
        Me.lblRepairRef.Text = "XXXXXX"
        Me.lblRepairRef.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(357, 231)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(141, 18)
        Me.Label14.TabIndex = 15
        Me.Label14.Text = "Fault Description:"
        '
        'lblCurrentRepairStatus
        '
        Me.lblCurrentRepairStatus.Location = New System.Drawing.Point(539, 302)
        Me.lblCurrentRepairStatus.Name = "lblCurrentRepairStatus"
        Me.lblCurrentRepairStatus.Size = New System.Drawing.Size(349, 18)
        Me.lblCurrentRepairStatus.TabIndex = 18
        Me.lblCurrentRepairStatus.Text = "<current repair status>"
        Me.lblCurrentRepairStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(357, 302)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(176, 18)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "Current Repair Status:"
        '
        'lblFaultDesc
        '
        Me.lblFaultDesc.Location = New System.Drawing.Point(539, 231)
        Me.lblFaultDesc.Name = "lblFaultDesc"
        Me.lblFaultDesc.Size = New System.Drawing.Size(457, 56)
        Me.lblFaultDesc.TabIndex = 19
        Me.lblFaultDesc.Text = "<fault description>"
        '
        'btnUpdateRepairStatus
        '
        Me.btnUpdateRepairStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.btnUpdateRepairStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateRepairStatus.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateRepairStatus.Location = New System.Drawing.Point(897, 300)
        Me.btnUpdateRepairStatus.Name = "btnUpdateRepairStatus"
        Me.btnUpdateRepairStatus.Size = New System.Drawing.Size(99, 25)
        Me.btnUpdateRepairStatus.TabIndex = 6
        Me.btnUpdateRepairStatus.Text = "Update Status"
        Me.btnUpdateRepairStatus.UseVisualStyleBackColor = False
        '
        'btnUpdateRepairQuote
        '
        Me.btnUpdateRepairQuote.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.btnUpdateRepairQuote.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateRepairQuote.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdateRepairQuote.Location = New System.Drawing.Point(897, 333)
        Me.btnUpdateRepairQuote.Name = "btnUpdateRepairQuote"
        Me.btnUpdateRepairQuote.Size = New System.Drawing.Size(99, 25)
        Me.btnUpdateRepairQuote.TabIndex = 20
        Me.btnUpdateRepairQuote.Text = "Update Quote"
        Me.btnUpdateRepairQuote.UseVisualStyleBackColor = False
        '
        'lblQuote
        '
        Me.lblQuote.Location = New System.Drawing.Point(539, 335)
        Me.lblQuote.Name = "lblQuote"
        Me.lblQuote.Size = New System.Drawing.Size(349, 18)
        Me.lblQuote.TabIndex = 22
        Me.lblQuote.Text = "<£quote>"
        Me.lblQuote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(357, 335)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(181, 18)
        Me.Label19.TabIndex = 21
        Me.Label19.Text = "Estimate Repair Quote:"
        '
        'dgvRepairRemarks
        '
        Me.dgvRepairRemarks.AllowUserToAddRows = False
        Me.dgvRepairRemarks.AllowUserToDeleteRows = False
        Me.dgvRepairRemarks.AllowUserToResizeColumns = False
        Me.dgvRepairRemarks.AllowUserToResizeRows = False
        Me.dgvRepairRemarks.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.dgvRepairRemarks.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvRepairRemarks.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvRepairRemarks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvRepairRemarks.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.colEmployee, Me.colTimestamp, Me.colRemark})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(55, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvRepairRemarks.DefaultCellStyle = DataGridViewCellStyle4
        Me.dgvRepairRemarks.GridColor = System.Drawing.Color.White
        Me.dgvRepairRemarks.Location = New System.Drawing.Point(360, 369)
        Me.dgvRepairRemarks.Name = "dgvRepairRemarks"
        Me.dgvRepairRemarks.ReadOnly = True
        Me.dgvRepairRemarks.RowHeadersVisible = False
        Me.dgvRepairRemarks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvRepairRemarks.Size = New System.Drawing.Size(636, 319)
        Me.dgvRepairRemarks.TabIndex = 23
        '
        'colEmployee
        '
        Me.colEmployee.HeaderText = "Employee"
        Me.colEmployee.Name = "colEmployee"
        Me.colEmployee.ReadOnly = True
        Me.colEmployee.Width = 150
        '
        'colTimestamp
        '
        Me.colTimestamp.HeaderText = "Date / Time"
        Me.colTimestamp.Name = "colTimestamp"
        Me.colTimestamp.ReadOnly = True
        Me.colTimestamp.Width = 125
        '
        'colRemark
        '
        Me.colRemark.HeaderText = "Remark"
        Me.colRemark.Name = "colRemark"
        Me.colRemark.ReadOnly = True
        Me.colRemark.Width = 355
        '
        'btnCustomerCollection
        '
        Me.btnCustomerCollection.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.btnCustomerCollection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCustomerCollection.Location = New System.Drawing.Point(850, 694)
        Me.btnCustomerCollection.Name = "btnCustomerCollection"
        Me.btnCustomerCollection.Size = New System.Drawing.Size(146, 31)
        Me.btnCustomerCollection.TabIndex = 6
        Me.btnCustomerCollection.Text = "Customer Collection"
        Me.btnCustomerCollection.UseVisualStyleBackColor = False
        '
        'btnAddRepairRemark
        '
        Me.btnAddRepairRemark.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.btnAddRepairRemark.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddRepairRemark.Location = New System.Drawing.Point(360, 694)
        Me.btnAddRepairRemark.Name = "btnAddRepairRemark"
        Me.btnAddRepairRemark.Size = New System.Drawing.Size(110, 31)
        Me.btnAddRepairRemark.TabIndex = 24
        Me.btnAddRepairRemark.Text = "Add Remark"
        Me.btnAddRepairRemark.UseVisualStyleBackColor = False
        '
        'frmRepairMgmt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(55, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.lblUserPrompt)
        Me.Controls.Add(Me.sidePanel)
        Me.Controls.Add(Me.btnAddRepairRemark)
        Me.Controls.Add(Me.btnCustomerCollection)
        Me.Controls.Add(Me.dgvRepairRemarks)
        Me.Controls.Add(Me.btnUpdateRepairQuote)
        Me.Controls.Add(Me.lblQuote)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.btnUpdateRepairStatus)
        Me.Controls.Add(Me.lblFaultDesc)
        Me.Controls.Add(Me.lblCurrentRepairStatus)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lblRepairRef)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblIntakeDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblHomePhn)
        Me.Controls.Add(Me.lblEmailAddress)
        Me.Controls.Add(Me.lblMobilePhn)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblAssetDetails)
        Me.Controls.Add(Me.lblCustomerNameAddress)
        Me.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.Name = "frmRepairMgmt"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Repairs Management"
        Me.sidePanel.ResumeLayout(False)
        Me.sidePanel.PerformLayout()
        CType(Me.dgvRepairSearchResults, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvRepairRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents sidePanel As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents btnSearchRepairs As Button
    Friend WithEvents tbSurname As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents tbRepairRef As TextBox
    Friend WithEvents dgvRepairSearchResults As DataGridView
    Friend WithEvents btnNewRepair As Button
    Friend WithEvents lblUserPrompt As Label
    Friend WithEvents lblCustomerNameAddress As Label
    Friend WithEvents lblAssetDetails As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblMobilePhn As Label
    Friend WithEvents lblEmailAddress As Label
    Friend WithEvents lblHomePhn As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents lblIntakeDate As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents lblRepairRef As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents lblCurrentRepairStatus As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents lblFaultDesc As Label
    Friend WithEvents btnUpdateRepairStatus As Button
    Friend WithEvents btnUpdateRepairQuote As Button
    Friend WithEvents lblQuote As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents dgvRepairRemarks As DataGridView
    Friend WithEvents btnCustomerCollection As Button
    Friend WithEvents btnAddRepairRemark As Button
    Friend WithEvents colEmployee As DataGridViewTextBoxColumn
    Friend WithEvents colTimestamp As DataGridViewTextBoxColumn
    Friend WithEvents colRemark As DataGridViewTextBoxColumn
    Friend WithEvents colRepairRef As DataGridViewTextBoxColumn
    Friend WithEvents colCustSurname As DataGridViewTextBoxColumn
    Friend WithEvents colAssetDetails As DataGridViewTextBoxColumn
    Friend WithEvents colRepairStatus As DataGridViewTextBoxColumn
End Class
