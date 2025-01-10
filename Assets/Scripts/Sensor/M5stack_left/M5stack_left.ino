#include <M5Stack.h>
#include "BluetoothSerial.h"

// BluetoothSerial インスタンスの作成
BluetoothSerial bts;

// 加速度
float accX = 0.0F, accY = 0.0F, accZ = 0.0F;

// 前回の加速度データ
float lastAccX = 0.0F;

// 閾値
const float threshold = 0.5;

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
  M5.Lcd.setCursor(140, 120);  // 画面中央にカーソル設定
  M5.Lcd.print("M5stack_left");
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
  // Serial.print(accX);
  // Serial.print(",");
  // Serial.print(accY);
  // Serial.print(",");
  // Serial.println(accZ);

  // 振動を検知（前回値との差分を計算）
  float diff = abs(accX - lastAccX);
  
  // 振動を検知した時の処理
  if (diff > threshold) {
    // debug
    Debug_vibration(); 
    Serial.println("{\"vibration\":true}");
  }

  // 前回の加速度値を更新
  lastAccX = accX;
  
  // 遅延
  delay(10); // 100ms
}

// 振動debug
void Debug_vibration() {
  // 画面を赤色で塗りつぶし、警告メッセージを表示
  M5.Lcd.fillScreen(RED);       // 画面を赤色で塗りつぶす
  M5.Lcd.setCursor(140, 120);   // 画面中央にカーソル設定
  M5.Lcd.setTextColor(WHITE, RED);
  M5.Lcd.print("振動あり");
  delay(500);                   // 短い遅延（0.5秒）

  // 画面を元に戻す
  M5.Lcd.fillScreen(BLACK);
  M5.Lcd.setCursor(140, 120);
  M5.Lcd.setTextColor(WHITE, BLACK);
  M5.Lcd.print("M5stack_left");
}