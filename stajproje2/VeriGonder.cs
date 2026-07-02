using stajproje2.Services;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading;
using System.Globalization;
using System.ComponentModel;

namespace stajproje2
{
    public partial class VeriGonder : Form
    {
        private readonly MqttService _mqttService = new MqttService();

        public VeriGonder()
        {
            InitializeComponent();
            btnGenerateJson.Click += btnGenerateJson_Click;
            this.Load += VeriGonder_Load;

        }

        private async void btnGenerateJson_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedType = cmbDataTypes.SelectedItem?.ToString();

                if (selectedType == "Her İkisi")
                {
                    string combinedJson = "";
                    foreach (var file in new[] { "OproTelemetry.json", "OproRaw.json" })
                    {
                        var path = Path.Combine(Application.StartupPath, "DataDummy", file);
                        if (!File.Exists(path)) continue;

                        var text = await File.ReadAllTextAsync(path);
                        var node = JsonNode.Parse(text)?.AsObject();
                        if (node == null) continue;

                        node["di"] = JsonValue.Create(txtMacAdress.Text.Trim());
                        node["ts"] = JsonValue.Create(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

                        var opts = new JsonSerializerOptions { WriteIndented = true };
                        combinedJson += node.ToJsonString(opts) + "\n\n--- RAW DATA ---\n\n";
                    }
                    txtJsonPayload.Text = combinedJson;
                    return;
                }

                var fileName = selectedType == "RawData" ? "OproRaw.json" : "OproTelemetry.json";

                var filePath = Path.Combine(Application.StartupPath, "DataDummy", fileName);

                if (!File.Exists(filePath))
                { 

                    string mesaj = Program.AktifDil == "en" ? $"File not found:" + filePath : $"Dosya bulunamadı: " + filePath;
                    string baslik = Program.AktifDil == "en" ? "Error" : "Hata";
                    MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                var jsonText = await File.ReadAllTextAsync(filePath);
                var root = JsonNode.Parse(jsonText)?.AsObject();

                if (root == null)
                {
                   
                    string mesaj = Program.AktifDil == "en" ? $"Couldn’t parse JSON!": $"JSON parse edilemedi! ";
                    string baslik = Program.AktifDil == "en" ? "Error" : "Hata";
                    MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                root["di"] = JsonValue.Create(txtMacAdress.Text.Trim());
                root["ts"] = JsonValue.Create(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

                var options = new JsonSerializerOptions { WriteIndented = true };
                txtJsonPayload.Text = root.ToJsonString(options);
            }
            catch (Exception ex)
            {
                
                string mesaj = Program.AktifDil == "en" ? $"Error: " + ex.Message : $"Hata: " + ex.Message;
                string baslik = Program.AktifDil == "en" ? "Error" : "Hata";
                MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task SendBothJson()
        {
            string combinedJson = "";

            foreach (var fileName in new[] { "OproTelemetry.json", "OproRaw.json" })
            {
                var filePath = Path.Combine(Application.StartupPath, "DataDummy", fileName);
                if (!File.Exists(filePath)) continue;

                var jsonText = await File.ReadAllTextAsync(filePath);
                var root = JsonNode.Parse(jsonText)?.AsObject();
                if (root == null) continue;

                root["di"] = JsonValue.Create(txtMacAdress.Text.Trim());
                root["ts"] = JsonValue.Create(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = root.ToJsonString(options);


                combinedJson += json + "\n\n--- RAW DATA ---\n\n";


                if (!_mqttService.IsConnected)
                    await _mqttService.ConnectAsync(//IP ADRESİ, //PORT, //KULLANICI ADI, //ŞİFRE);

                var topic = $"v1/devices/{txtMacAdress.Text.Trim()}/attributes";
                await _mqttService.PublishAsync(topic, json);
            }

            txtJsonPayload.Text = combinedJson;
          
            string mesaj = Program.AktifDil == "en" ? $"Both sets of data have been sent!" : $"Her iki veri seti de gönderildi!";
            string baslik = Program.AktifDil == "en" ? "Success" : "Başarılı";
            MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            if (cmbDataTypes.SelectedItem?.ToString() == "Her İkisi" || cmbDataTypes.SelectedItem?.ToString() == "Both")
            {
                await SendBothJson();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMacAdress.Text) || txtMacAdress.Text == "Mac Adres")
            {
                string mesaj = Program.AktifDil == "en" ? $"MAC Address cannot be empty!" : $"MAC Adresi boş olamaz!";
                string baslik = Program.AktifDil == "en" ? "Warning" : "Uyarı";
                MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtJsonPayload.Text))
            {
                string mesaj = Program.AktifDil == "en" ? $"First, press the Prepare JSON button!" : $"Önce JSON Hazırla butonuna bas!";
                string baslik = Program.AktifDil == "en" ? "Warning" : "Uyarı";
                MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var jsonCheck = JsonNode.Parse(txtJsonPayload.Text)?.AsObject();
                var macFromJson = jsonCheck?["di"]?.GetValue<string>() ?? "";

                
                if (string.IsNullOrWhiteSpace(macFromJson))
                {
                    string bosMacMesaji = Program.AktifDil == "en" ? $"Error: The MAC Address in the JSON is empty or corrupted! Please enter a MAC address or reprepare the JSON." : $"Hata: JSON içindeki MAC Adresi (di) boş ya da di bozulmuş! Lütfen bir MAC adresi yazın veya yeniden JSON Hazırlayın. ";
                    string bosMacBaslik = Program.AktifDil == "en" ? "Error" : "Uyarı";
                    MessageBox.Show(bosMacMesaji, bosMacBaslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                if (macFromJson != txtMacAdress.Text.Trim())
                {
                    string uyariMesaji = Program.AktifDil == "en" ? $"Warning: The MAC Address on the left does not match the MAC Address you entered in the JSON!" : $"Dikkat: Soldaki MAC Adresi ile JSON içine yazdığınız MAC Adresi eşleşmiyor! ";
                    string uyariBaslik = Program.AktifDil == "en" ? "Warning" : "Uyarı";
                    MessageBox.Show(uyariMesaji, uyariBaslik, MessageBoxButtons.OK, MessageBoxIcon. Warning);
                    return;
                }

                
                int count = (int)numcounter.Value;
                if (count < 1) count = 1;


                btnSendMqtt.Enabled = false;

                
                if (!_mqttService.IsConnected)
                    await _mqttService.ConnectAsync(//IP ADRESİ, //PORT, //KULLANICI ADI, //ŞİFRE);

                var topic = $"v1/devices/{macFromJson}/attributes";

                List<Task> threadHavuzu = new List<Task>();


                string gonderilecekJson = txtJsonPayload.Text;

          
                for (int i = 0; i < count; i++)
                {
                    threadHavuzu.Add(Task.Run(async () =>
                    {
                        
                        await _mqttService.PublishAsync(topic, gonderilecekJson);
                    }));
                }


                await Task.WhenAll(threadHavuzu);

                string basariMesaji;
                string basariBaslik = Program.AktifDil == "en" ? "Success" : "Başarılı";

                if (Program.AktifDil == "en")
                {
                    basariMesaji = count == 1
                        ? "Data sent successfully!"
                        : $"Data sent {count} times successfully!";
                }
                else
                {
                    basariMesaji = count == 1
                        ? "Veri başarıyla gönderildi!"
                        : $"Veri {count} kez başarıyla gönderildi!";
                }

                MessageBox.Show(basariMesaji, basariBaslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Text.Json.JsonException)
            {
                string mesaj = Program.AktifDil == "en" ? $"The JSON format is corrupted! Make sure you didn’t delete any curly braces or quotes while deleting." : $"JSON formatı bozulmuş! Lütfen silerken süslü parantez veya tırnakları silmediğinizden emin olun. ";
                string baslik = Program.AktifDil == "en" ? "Error" : "Hata";
                MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                string mesaj = Program.AktifDil == "en" ? $"Error: " + ex.Message : $"Hata: " + ex.Message;
                string baslik = Program.AktifDil == "en" ? "Error" : "Hata";
                MessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                btnSendMqtt.Enabled = true;
            }
        }

        private void aLARMOLUŞTURToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var alarmForm = new AlarmGonder();
            alarmForm.Show();
        }

        private void txtJsonPayload_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbLanguage.SelectedItem == null || cmbLanguage.SelectedItem.ToString() == "Language")
                return;

            string secilenDil = cmbLanguage.SelectedItem.ToString();

            if (secilenDil == "EN")
            {
                Program.AktifDil = "en";     
                DiliDegistir("en");         
            }
            else if (secilenDil == "TR")
            {
                Program.AktifDil = "tr-TR";  
                DiliDegistir("tr-TR");       
            }

        }

        private void lblDataType_Click(object sender, EventArgs e)
        {

        }

        private void lblMacAdress_Click(object sender, EventArgs e)
        {

        }

        private void DiliDegistir(string dilKodu)
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(dilKodu);


            ComponentResourceManager resources = new ComponentResourceManager(typeof(VeriGonder));


            resources.ApplyResources(this, "$this");


            foreach (Control c in this.Controls)
            {
                resources.ApplyResources(c, c.Name);
            }

            if (menuStrip2 != null)
            {
                foreach (ToolStripItem item in menuStrip2.Items)
                {
                    resources.ApplyResources(item, item.Name);
                }
            }

            cmbDataTypes.Items.Clear();
            if (dilKodu == "en")
                cmbDataTypes.Items.AddRange(new object[] { "TelemetryData", "RawData", "Both" });
            else
                cmbDataTypes.Items.AddRange(new object[] { "TelemetryData", "RawData", "Her İkisi" });
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void VeriGonder_Load(object sender, EventArgs e)
        {
            cmbLanguage.Items.Clear();
            cmbLanguage.Items.AddRange(new object[] { "TR", "EN" });

            cmbDataTypes.Items.Clear();
            if (Program.AktifDil == "en")
                cmbDataTypes.Items.AddRange(new object[] { "TelemetryData", "RawData", "Both" });
            else
                cmbDataTypes.Items.AddRange(new object[] { "TelemetryData", "RawData", "Her İkisi" });
        }

    }
}