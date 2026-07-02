using stajproje2.Services;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace stajproje2
{
    public partial class AlarmGonder : Form
    {
        private readonly MqttService _mqttService = new MqttService();

        public AlarmGonder()
        {
            InitializeComponent();
            nudWarningLimit.ValueChanged += nudWarningLimit_ValueChanged;
            btnTriggerAlarm.Click += btnTriggerAlarm_Click;
            btnGenerateAlarmJson.Click += btnGenerateAlarmJson_Click;
        }

        private void nudWarningLimit_ValueChanged(object? sender, EventArgs e)
        {
            nudMonitorLimit.Value = Math.Round(nudWarningLimit.Value * 0.6m, 2);
        }

        private async void btnGenerateAlarmJson_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = Path.Combine(Application.StartupPath, "DataDummy", "OproTelemetry.json");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Dosya bulunamadı: " + filePath, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var jsonText = await File.ReadAllTextAsync(filePath);
                var root = JsonNode.Parse(jsonText)?.AsObject();

                if (root == null)
                {
                    MessageBox.Show("JSON parse edilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                root["di"] = JsonValue.Create(txtMacAddress.Text.Trim());
                root["ts"] = JsonValue.Create(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

                var values = root["values"]?.AsObject();
                if (values != null)
                {

                    
                    string secilenSeviye = dangerWarningSelect.SelectedItem?.ToString() ?? "Warning";

                    
                    double basilacakDeger = 0;

                    if (secilenSeviye == "Warning")
                    {
                        basilacakDeger = (double)nudWarningLimit.Value;
                    }
                    else if (secilenSeviye == "Danger")
                    {
                        basilacakDeger = (double)nudDangerLimit.Value;
                    }


                    string secilenAlarm = cmbAlarmType.SelectedItem?.ToString() ?? "Sıcaklık";


                    if (secilenAlarm == "Sıcaklık" || secilenAlarm == "Temperature")
                    {
                        values["temp"] = JsonValue.Create(basilacakDeger);
                    }
                    else if (secilenAlarm == "Titreşim Hız RMS" || secilenAlarm == "Vibration Velocity RMS")
                    {
                        values["maxVRMS"] = JsonValue.Create(basilacakDeger);
                        values["xVRMS"] = JsonValue.Create(basilacakDeger);
                        values["yVRMS"] = JsonValue.Create(basilacakDeger);
                        values["zVRMS"] = JsonValue.Create(basilacakDeger);
                    }
                    else if (secilenAlarm == "Titreşim İvme RMS" || secilenAlarm == "Vibration Acceleration RMS")
                    {
                        values["maxRMS"] = JsonValue.Create(basilacakDeger);
                        values["xRMS"] = JsonValue.Create(basilacakDeger);
                        values["yRMS"] = JsonValue.Create(basilacakDeger);
                        values["zRMS"] = JsonValue.Create(basilacakDeger);
                    }
                }

                var options = new JsonSerializerOptions { WriteIndented = true };
                txtAlarmJsonPayload.Text = root.ToJsonString(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }

        private async void btnTriggerAlarm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMacAddress.Text))
            {
        
                string mesaj = Program.AktifDil == "en" ? $"MAC address can't be empty!" : $"MAC Adresi boş olamaz!";
                string baslik = Program.AktifDil == "en" ? "Warning" : "Uyarı";
                MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(txtAlarmJsonPayload.Text))
            {
                
                string mesaj = Program.AktifDil == "en" ? $"First, press the Prepare JSON button!" : $"Önce JSON Hazırla butonuna bas!";
                string baslik = Program.AktifDil == "en" ? "Warning" : "Uyarı";
                MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                var jsonCheck = JsonNode.Parse(txtAlarmJsonPayload.Text)?.AsObject();
                var macFromJson = jsonCheck?["di"]?.GetValue<string>() ?? "";

                if (string.IsNullOrWhiteSpace(macFromJson) || macFromJson != txtMacAddress.Text.Trim())
                {
  

                    string uyariMesaji = Program.AktifDil == "en" ? $"The MAC Address in the JSON doesn't match the entered MAC Address!" : $"JSON içindeki MAC Adresi ile girilen MAC Adresi eşleşmiyor!";
                    string uyariBaslik = Program.AktifDil == "en" ? "Warning" : "Uyarı";
                    MessageBox.Show(uyariMesaji, uyariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                int count = (int)nudRetryCount.Value;
                if (count < 1) count = 1;
                btnTriggerAlarm.Enabled = false;

                if (!_mqttService.IsConnected)
                    await _mqttService.ConnectAsync(//IP, //PORT, //USERNAME, //ŞİFRE);

                var topic = $"v1/devices/{txtMacAddress.Text.Trim()}/attributes";

                for (int i = 0; i < count; i++)
                {
                    await _mqttService.PublishAsync(topic, txtAlarmJsonPayload.Text);
                    await Task.Delay(1000);
                }

                string basariMesaji;
                string basariBaslik = Program.AktifDil == "en" ? "Success" : "Başarılı";

                if (Program.AktifDil == "en")
                {
                    basariMesaji = count == 1
                        ? "Alarm sent successfully!"
                        : $"Alarm sent {count} times successfully!";
                }
                else
                {
                    basariMesaji = count == 1
                        ? "Alarm başarıyla gönderildi!"
                        : $"Alarm {count} kez başarıyla gönderildi!";
                }

                MessageBox.Show(basariMesaji, basariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
               
                btnTriggerAlarm.Enabled = true;
                MessageBox.Show(" Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                btnTriggerAlarm.Enabled = true;
            }

        }

        private void AlarmGonder_Load(object sender, EventArgs e)
        {
            dangerWarningSelect.Items.Clear();
            dangerWarningSelect.Items.AddRange(new object[] { "Warning", "Danger" });
            if (dangerWarningSelect.Items.Count > 0) dangerWarningSelect.SelectedIndex = 0;

            
            cmbAlarmType.Items.Clear();
            if (Program.AktifDil == "en")
            {
                cmbAlarmType.Items.AddRange(new object[]
                {
            "Temperature",
            "Vibration Velocity RMS",
            "Vibration Acceleration RMS"
                });
            }
            else
            {
                cmbAlarmType.Items.AddRange(new object[]
                {
            "Sıcaklık",
            "Titreşim Hız RMS",
            "Titreşim İvme RMS"
                });
            }
            cmbAlarmType.SelectedIndex = 0;
        }



        private void cmbAlarmType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}