namespace stajproje2
{
    partial class VeriGonder
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VeriGonder));
            cmbDataTypes = new ComboBox();
            btnGenerateJson = new Button();
            btnSendMqtt = new Button();
            txtMacAdress = new TextBox();
            menuStrip2 = new MenuStrip();
            vERİGÖNDERToolStripMenuItem = new ToolStripMenuItem();
            aLARMOLUŞTURToolStripMenuItem = new ToolStripMenuItem();
            lblMacAdress = new Label();
            lblDataType = new Label();
            cmbLanguage = new ComboBox();
            label1 = new Label();
            txtJsonPayload = new RichTextBox();
            numcounter = new NumericUpDown();
            label2 = new Label();
            menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numcounter).BeginInit();
            SuspendLayout();
            // 
            // cmbDataTypes
            // 
            resources.ApplyResources(cmbDataTypes, "cmbDataTypes");
            cmbDataTypes.FormattingEnabled = true;
            cmbDataTypes.Items.AddRange(new object[] { resources.GetString("cmbDataTypes.Items"), resources.GetString("cmbDataTypes.Items1") });
            cmbDataTypes.Name = "cmbDataTypes";
            // 
            // btnGenerateJson
            // 
            resources.ApplyResources(btnGenerateJson, "btnGenerateJson");
            btnGenerateJson.BackColor = Color.Silver;
            btnGenerateJson.Name = "btnGenerateJson";
            btnGenerateJson.UseVisualStyleBackColor = false;
            // 
            // btnSendMqtt
            // 
            resources.ApplyResources(btnSendMqtt, "btnSendMqtt");
            btnSendMqtt.BackColor = Color.OliveDrab;
            btnSendMqtt.Name = "btnSendMqtt";
            btnSendMqtt.UseVisualStyleBackColor = false;
            btnSendMqtt.Click += button2_Click;
            // 
            // txtMacAdress
            // 
            resources.ApplyResources(txtMacAdress, "txtMacAdress");
            txtMacAdress.BorderStyle = BorderStyle.None;
            txtMacAdress.Name = "txtMacAdress";
            // 
            // menuStrip2
            // 
            resources.ApplyResources(menuStrip2, "menuStrip2");
            menuStrip2.BackColor = Color.DimGray;
            menuStrip2.ImageScalingSize = new Size(24, 24);
            menuStrip2.Items.AddRange(new ToolStripItem[] { vERİGÖNDERToolStripMenuItem, aLARMOLUŞTURToolStripMenuItem });
            menuStrip2.Name = "menuStrip2";
            // 
            // vERİGÖNDERToolStripMenuItem
            // 
            resources.ApplyResources(vERİGÖNDERToolStripMenuItem, "vERİGÖNDERToolStripMenuItem");
            vERİGÖNDERToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            vERİGÖNDERToolStripMenuItem.Name = "vERİGÖNDERToolStripMenuItem";
            // 
            // aLARMOLUŞTURToolStripMenuItem
            // 
            resources.ApplyResources(aLARMOLUŞTURToolStripMenuItem, "aLARMOLUŞTURToolStripMenuItem");
            aLARMOLUŞTURToolStripMenuItem.BackColor = Color.DimGray;
            aLARMOLUŞTURToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            aLARMOLUŞTURToolStripMenuItem.Name = "aLARMOLUŞTURToolStripMenuItem";
            aLARMOLUŞTURToolStripMenuItem.Click += aLARMOLUŞTURToolStripMenuItem_Click;
            // 
            // lblMacAdress
            // 
            resources.ApplyResources(lblMacAdress, "lblMacAdress");
            lblMacAdress.Name = "lblMacAdress";
            lblMacAdress.Click += lblMacAdress_Click;
            // 
            // lblDataType
            // 
            resources.ApplyResources(lblDataType, "lblDataType");
            lblDataType.Name = "lblDataType";
            lblDataType.Click += lblDataType_Click;
            // 
            // cmbLanguage
            // 
            resources.ApplyResources(cmbLanguage, "cmbLanguage");
            cmbLanguage.BackColor = Color.WhiteSmoke;
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Items.AddRange(new object[] { resources.GetString("cmbLanguage.Items"), resources.GetString("cmbLanguage.Items1"), resources.GetString("cmbLanguage.Items2") });
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // txtJsonPayload
            // 
            resources.ApplyResources(txtJsonPayload, "txtJsonPayload");
            txtJsonPayload.BorderStyle = BorderStyle.None;
            txtJsonPayload.Name = "txtJsonPayload";
            // 
            // numcounter
            // 
            resources.ApplyResources(numcounter, "numcounter");
            numcounter.BackColor = Color.WhiteSmoke;
            numcounter.BorderStyle = BorderStyle.None;
            numcounter.Name = "numcounter";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            label2.Click += label2_Click_1;
            // 
            // VeriGonder
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            Controls.Add(label2);
            Controls.Add(numcounter);
            Controls.Add(txtJsonPayload);
            Controls.Add(label1);
            Controls.Add(cmbLanguage);
            Controls.Add(lblDataType);
            Controls.Add(lblMacAdress);
            Controls.Add(txtMacAdress);
            Controls.Add(btnSendMqtt);
            Controls.Add(btnGenerateJson);
            Controls.Add(cmbDataTypes);
            Controls.Add(menuStrip2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "VeriGonder";
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numcounter).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ComboBox cmbDataTypes;
        private Button btnGenerateJson;
        private Button btnSendMqtt;
        private TextBox txtMacAdress;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem vERİGÖNDERToolStripMenuItem;
        private ToolStripMenuItem aLARMOLUŞTURToolStripMenuItem;
        private Label lblMacAdress;
        private Label lblDataType;
        private RichTextBox txtJsonPayload;
        private ComboBox cmbLanguage;
        private Label label1;
        private NumericUpDown numcounter;
        private Label label2;
    }
}