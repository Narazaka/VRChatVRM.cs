# VRChatVRM.cs

VRChatのアバターをそのままVRMとして使う時の雑なコンポーネント付け外しスクリプト。

VRM関連のコンポーネントが付いてるとVRChatでビルドできないんでむしゃくしゃして作った。

## こういうやつ

![スクショ](image.png)

## 使い方

0. Editorフォルダをどこかに作って[`VRChatVRM.cs`](VRChatVRM.cs)をてきとうにUnityにつっこむ。UniVRM入ってる前提。
1. VRMMetaObjectがアセットのメニューから作れるようになってるんで、そこに説明とか色々書く。
2. BlendShapeもアセットメニューから作成しできる。デフォルト表情系はBlendShapeファイルを選択してインスペクター右上のメニューから「CreateDefaultPresets」で作れる。
3. VRChatに突っ込むときはVRMはずす、VRM出力するときはVRM適用してTポーズとかのチェック入れて出力がよさげかと。

## 注意

自作モデルを凄く雑な感じでVRMに出力するためのやつなんで、上手くいかないとかは普通にあると思います。

## DynamicBoneとSpringBoneの変換はどうするか？

[[Unityエディタ拡張]ReflectBoneSetting](https://mrslip777.booth.pm/items/1596377)がそれっぽい。

## ライセンス

[NYSL](http://www.kmonos.net/nysl/)
