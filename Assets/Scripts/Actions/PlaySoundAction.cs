///////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2015 AsNet Co., Ltd.
// All Rights Reserved. These instructions, statements, computer
// programs, and/or related material (collectively, the "Source")
// contain unpublished information proprietary to AsNet Co., Ltd
// which is protected by US federal copyright law and by
// international treaties. This Source may NOT be disclosed to
// third parties, or be copied or duplicated, in whole or in
// part, without the written consent of AsNet Co., Ltd.
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class PlaySoundAction : BaseAction
{
    // The sound ID
    private SoundID _soundId;

    // The sound type
    private SoundType _soundType;

    // The delay time
    private float _delay;

    public PlaySoundAction(SoundID soundId, SoundType soundType, float delay)
    {
        // Set sound ID
        _soundId = soundId;

        // Set sound type
        _soundType = soundType;

        // Set delay time
        _delay = delay;
    }

    public static PlaySoundAction Create(SoundID soundId, SoundType soundType = SoundType.Replace, float delay = 0)
    {
        return new PlaySoundAction(soundId, soundType, delay);
    }

    public override void Play(GameObject target)
    {
        SoundManager.Instance.PlaySound(_soundId, _soundType, _delay);
    }
}
