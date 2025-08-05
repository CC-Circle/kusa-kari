#include <M5Unified.h>
#include <WiFi.h>
#include <Arduino.h>
#include <SPI.h>
#include "udp_sender.h"

// LEDマトリックスの色を指定しやすくするヘルパー関数
uint32_t dispColor(uint8_t r, uint8_t g, uint8_t b)
{
  return (r << 16) | (g << 8) | b;
}

// -------- ユーザー設定項目 --------
// 接続するWi-FiのSSIDとパスワード
const char *ssid = "Buffalo-G-F9D0";
const char *password = "nreja7brbdmw5";

// 送信先のIPアドレスとポート番号
// const IPAddress targetIP(192, 168, 0, 140); // 受信側PCのIPアドレス
const IPAddress targetIPs[] = {
    IPAddress(192, 168, 0, 140),
    IPAddress(192, 168, 0, 26),
    IPAddress(192, 168, 0, 99),
};

const int targetPort = 12345;
// ------------------------------------

void setup()
{
  auto cfg = M5.config();
  M5.begin(cfg);

  // 【修正】LEDマトリックスを初期化中/Wi-Fi未接続を示す紫色に点灯
  M5.Display.fillScreen(dispColor(128, 0, 128));
  delay(50);

  Serial.begin(115200);

  // --- Wi-Fi接続処理 ---
  M5.Lcd.println("Connecting to WiFi...");
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
    M5.Lcd.print(".");
  }

  Serial.println("\nWiFi connected!");
  Serial.print("IP Address: ");
  Serial.println(WiFi.localIP());

  M5.Lcd.fillScreen(TFT_BLACK);
  M5.Lcd.setCursor(10, 10);
  M5.Lcd.printf("WiFi Connected!\nIP: %s", WiFi.localIP().toString().c_str());
  delay(2000);
}

void loop()
{
  M5.update();

  if (WiFi.status() != WL_CONNECTED)
  {
    // 【修正】Wi-Fiが切断された場合、LEDマトリックスを紫色にしてエラー表示
    M5.Display.fillScreen(dispColor(128, 0, 128));
    Serial.println("WiFi not connected");
  }
  else
  {
    // 【修正】Wi-Fiが接続されている場合、LEDマトリックスを緑色に点灯
    M5.Display.fillScreen(dispColor(0, 128, 0));

    // IMUから加速度データを取得
    float ax, ay, az;
    M5.Imu.getAccel(&ax, &ay, &az);

    // 加速度データをシリアルモニターに表示
    Serial.printf("Accel: X=%.2f, Y=%.2f, Z=%.2f (g)\n", ax, ay, az);

    // 送信するメッセージをJSON形式で作成
    char message[128];
    snprintf(message, sizeof(message), "{\"accel\":{\"x\":%.2f,\"y\":%.2f,\"z\":%.2f}}", ax, ay, az);

    // 送信先のIPアドレスとポート番号にメッセージを送信
    sendUdpMessage(targetIPs[0], targetPort, message);
    for (int i = 0; i < 3; i++)
    {
      sendUdpMessage(targetIPs[i], targetPort, message);
      delay(500);
    }
  }

  delay(500); // 0.5秒ごとに送信
}