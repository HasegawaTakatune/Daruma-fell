﻿
/// <summary>
/// ゲームステータス
/// </summary>
public enum GAME_STATE
{
    /// <summary>
    /// メニュー
    /// </summary>
    MENU = 0,

    /// <summary>
    /// プレイング
    /// </summary>
    PLAYING,

    /// <summary>
    /// ゲームエンド
    /// </summary>
    END,

    /// <summary>
    /// Length
    /// </summary>
    LENGTH
}

/// <summary>
/// ライト
/// </summary>
public enum LIGHT
{
    /// <summary>
    /// ライトアップ
    /// </summary>
    LIGHT_UP = 0,

    /// <summary>
    /// 点滅
    /// </summary>
    FLASHING,

    /// <summary>
    /// 消灯
    /// </summary>
    OFF,

    /// <summary>
    /// Length
    /// </summary>
    LENGTH
}

/// <summary>
/// 幽霊のモード
/// </summary>
public enum GHOST_MODE
{
    /// <summary>
    /// 移動
    /// </summary>
    MOVE = 0,

    /// <summary>
    /// 停止
    /// </summary>
    STOP,

    /// <summary>
    /// 反則　振り向いた
    /// </summary>
    FOUL_TURNED_AROUND,

    /// <summary>
    /// 反則　振り向かなかった
    /// </summary>
    FOUL_NOT_TURN_AROUND,

    /// <summary>
    /// 反則
    /// </summary>
    FOUL,

    /// <summary>
    /// 機能しない
    /// </summary>
    NO_FUNCTION,

    /// <summary>
    /// ゲームセット
    /// </summary>
    GAME_SET,

    /// <summary>
    /// Length
    /// </summary>
    LENGTH
}
