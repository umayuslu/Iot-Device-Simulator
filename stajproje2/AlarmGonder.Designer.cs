namespace stajproje2
{
    partial class AlarmGonder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlarmGonder));
            btnGenerateAlarmJson = new Button();
            btnTriggerAlarm = new Button();
            txtAlarmJsonPayload = new TextBox();
            txtMacAddress = new TextBox();
            nudWarningLimit = new NumericUpDown();
            nudMonitorLimit = new NumericUpDown();
            nudRetryCount = new NumericUpDown();
            nudDangerLimit = new NumericUpDown();
            cmbAlarmType = new ComboBox();
            lblDangerLimit = new Label();
            lblWarningLimit = new Label();
            lblMonitorLimit = new Label();
            lblRetryCount = new Label();
            lblMacAddress = new Label();
            lblAlarmType = new Label();
            dangerWarningSelect = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)nudWarningLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMonitorLimit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRetryCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDangerLimit).BeginInit();
            SuspendLayout();
            // 
            // btnGenerateAlarmJson
            // 
            resources.ApplyResources(btnGenerateAlarmJson, "btnGenerateAlarmJson");
            btnGenerateAlarmJson.BackColor = Color.Silver;
            btnGenerateAlarmJson.Name = "btnGenerateAlarmJson";
            btnGenerateAlarmJson.UseVisualStyleBackColor = false;
            // 
            // btnTriggerAlarm
            // 
            resources.ApplyResources(btnTriggerAlarm, "btnTriggerAlarm");
            btnTriggerAlarm.BackColor = Color.DarkRed;
            btnTriggerAlarm.Name = "btnTriggerAlarm";
            btnTriggerAlarm.UseVisualStyleBackColor = false;
            // 
            // txtAlarmJsonPayload
            // 
            resources.ApplyResources(txtAlarmJsonPayload, "txtAlarmJsonPayload");
            txtAlarmJsonPayload.BorderStyle = BorderStyle.None;
            txtAlarmJsonPayload.Name = "txtAlarmJsonPayload";
            // 
            // txtMacAddress
            // 
            resources.ApplyResources(txtMacAddress, "txtMacAddress");
            txtMacAddress.BorderStyle = BorderStyle.None;
            txtMacAddress.Name = "txtMacAddress";
            // 
            // nudWarningLimit
            // 
            resources.ApplyResources(nudWarningLimit, "nudWarningLimit");
            nudWarningLimit.BackColor = Color.WhiteSmoke;
            nudWarningLimit.BorderStyle = BorderStyle.None;
            nudWarningLimit.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudWarningLimit.Name = "nudWarningLimit";
            // 
            // nudMonitorLimit
            // 
            resources.ApplyResources(nudMonitorLimit, "nudMonitorLimit");
            nudMonitorLimit.BackColor = Color.WhiteSmoke;
            nudMonitorLimit.BorderStyle = BorderStyle.None;
            nudMonitorLimit.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudMonitorLimit.Name = "nudMonitorLimit";
            nudMonitorLimit.ReadOnly = true;
            // 
            // nudRetryCount
            // 
            resources.ApplyResources(nudRetryCount, "nudRetryCount");
            nudRetryCount.BackColor = Color.WhiteSmoke;
            nudRetryCount.BorderStyle = BorderStyle.None;
            nudRetryCount.Name = "nudRetryCount";
            // 
            // nudDangerLimit
            // 
            resources.ApplyResources(nudDangerLimit, "nudDangerLimit");
            nudDangerLimit.BackColor = Color.WhiteSmoke;
            nudDangerLimit.BorderStyle = BorderStyle.None;
            nudDangerLimit.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudDangerLimit.Name = "nudDangerLimit";
            // 
            // cmbAlarmType
            // 
            resources.ApplyResources(cmbAlarmType, "cmbAlarmType");
            cmbAlarmType.FormattingEnabled = true;
            cmbAlarmType.Name = "cmbAlarmType";
            cmbAlarmType.SelectedIndexChanged += cmbAlarmType_SelectedIndexChanged;
            // 
            // lblDangerLimit
            // 
            resources.ApplyResources(lblDangerLimit, "lblDangerLimit");
            lblDangerLimit.Name = "lblDangerLimit";
            // 
            // lblWarningLimit
            // 
            resources.ApplyResources(lblWarningLimit, "lblWarningLimit");
            lblWarningLimit.Name = "lblWarningLimit";
            // 
            // lblMonitorLimit
            // 
            resources.ApplyResources(lblMonitorLimit, "lblMonitorLimit");
            lblMonitorLimit.Name = "lblMonitorLimit";
            // 
            // lblRetryCount
            // 
            resources.ApplyResources(lblRetryCount, "lblRetryCount");
            lblRetryCount.Name = "lblRetryCount";
            // 
            // lblMacAddress
            // 
            resources.ApplyResources(lblMacAddress, "lblMacAddress");
            lblMacAddress.Name = "lblMacAddress";
            // 
            // lblAlarmType
            // 
            resources.ApplyResources(lblAlarmType, "lblAlarmType");
            lblAlarmType.Name = "lblAlarmType";
            // 
            // dangerWarningSelect
            // 
            resources.ApplyResources(dangerWarningSelect, "dangerWarningSelect");
            dangerWarningSelect.FormattingEnabled = true;
            dangerWarningSelect.Items.AddRange(new object[] { resources.GetString("dangerWarningSelect.Items"), resources.GetString("dangerWarningSelect.Items1") });
            dangerWarningSelect.Name = "dangerWarningSelect";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // AlarmGonder
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            Controls.Add(label1);
            Controls.Add(dangerWarningSelect);
            Controls.Add(lblMacAddress);
            Controls.Add(txtMacAddress);
            Controls.Add(lblAlarmType);
            Controls.Add(cmbAlarmType);
            Controls.Add(lblDangerLimit);
            Controls.Add(nudDangerLimit);
            Controls.Add(lblWarningLimit);
            Controls.Add(nudWarningLimit);
            Controls.Add(lblMonitorLimit);
            Controls.Add(nudMonitorLimit);
            Controls.Add(lblRetryCount);
            Controls.Add(nudRetryCount);
            Controls.Add(txtAlarmJsonPayload);
            Controls.Add(btnTriggerAlarm);
            Controls.Add(btnGenerateAlarmJson);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AlarmGonder";
            Load += AlarmGonder_Load;
            ((System.ComponentModel.ISupportInitialize)nudWarningLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMonitorLimit).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRetryCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDangerLimit).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Button btnGenerateAlarmJson;
        private Button btnTriggerAlarm;
        private TextBox txtAlarmJsonPayload;
        private TextBox txtMacAddress;
        private NumericUpDown nudWarningLimit;
        private NumericUpDown nudMonitorLimit;
        private NumericUpDown nudRetryCount;
        private NumericUpDown nudDangerLimit;
        private ComboBox cmbAlarmType;
        private Label lblDangerLimit;
        private Label lblWarningLimit;
        private Label lblMonitorLimit;
        private Label lblRetryCount;
        private Label lblMacAddress;
        private Label lblAlarmType;
        private ComboBox dangerWarningSelect;
        private Label label1;
    }
}