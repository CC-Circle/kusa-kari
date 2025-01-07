#include <M5Stack.h>
#include "BluetoothSerial.h"

// BluetoothSerial インスタンスの作成
BluetoothSerial bts;

// 加速度
float accX = 0.0F, accY = 0.0F, accZ = 0.0F;

void setup() {
  // M5Stackの初期化
  M5.begin();
  M5.Power.begin();

  // Bluetoothの初期化（M5stack_01,02...）
  // bts.begin("M5Stack_01");

  // IMU（加速度センサー）の初期化
  M5.IMU.Init();

  // 画面の初期化
  M5.Lcd.fillScreen(BLACK);
  M5.Lcd.setTextColor(WHITE, BLACK);  // 白色文字、黒背景
  M5.Lcd.setTextSize(2);
  M5.Lcd.setTextDatum(MC_DATUM);  // 文字の位置を中央に設定

  // フォントの変更
  M5.Lcd.setTextFont(2);

  // "M5stack_01" を画面中央に表示
  M5.Lcd.setCursor(160, 120);  // 画面中央にカーソル設定
  M5.Lcd.print("M5stack_01");
}

void loop() {
  // 加速度データを取得
  M5.IMU.getAccelData(&accX, &accY, &accZ);

  // Bluetoothで加速度データを送信
  // bts.print(accX);
  // bts.print(",");
  // bts.print(accY);
  // bts.print(",");
  // bts.println(accZ);

  // シリアルで加速度データを表示
  Serial.print(accX);
  Serial.print(",");
  Serial.print(accY);
  Serial.print(",");
  Serial.println(accZ);

  // 遅延
  delay(10); // 100ms
}