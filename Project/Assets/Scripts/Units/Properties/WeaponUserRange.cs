using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUserRange : WeaponUserBase
{

    public List<InputUid> swapInputUids;
    public List<WeaponSpawnDesc> weaponDescs;

    public void Init(PoolBase pool)
    {
        base.Init();
        
        this.pool = pool;

        foreach (WeaponSpawnDesc desc in weaponDescs)
        {
            var weapon = Instantiate(desc.prefab, desc.transform.position, desc.transform.rotation);
            var weaponLogic = weapon.GetComponent<WeaponRange>();
            weapon.transform.SetParent(desc.transform);
            weaponLogic.Init(pool);
            weapons.Add(weaponLogic);
        }

        currentWeaponIdx = 0;
        currentWeapon = weapons[currentWeaponIdx];
        currentWeapon.Enable();
        currentWeapon.OnShootEmpty.AddListener(Reload);
    }

    public override void Terminate()
    {
        currentWeapon.Terminate();

        base.Terminate();
    }

    public override void CacheInputsLibrary(InputsLibrary inputsLibrary)
    {
        base.CacheInputsLibrary(inputsLibrary);

        var input1 = (InputTap)inputsLibrary.GetInput(InputUid.SLOT_1);
        input1.OnUse.AddListener(Debugg);
        var input2 = (InputTap)inputsLibrary.GetInput(InputUid.SLOT_2);
        input2.OnUse.AddListener(Debugg);
        var input3 = (InputTap)inputsLibrary.GetInput(InputUid.SLOT_3);
        input3.OnUse.AddListener(Debugg);

        var idx = 0;
        foreach(InputUid uid in swapInputUids)
        {
            var slotIdx = idx;
            var input = (InputTap)inputsLibrary.GetInput(uid);
            input.OnUse.AddListener(() => SwapToInput(slotIdx));
            swapToSlotInputs.Add(input);
            idx += 1;
        }
    }

    public override void RequestAttack()
    {
        base.RequestAttack();

        currentWeapon.RequestAttack();
    }

    public override void RequestStopAttack()
    {
        currentWeapon.RequestStopAttack();

        base.RequestStopAttack();
    }

    #region private

    List<WeaponRange> weapons = new List<WeaponRange>();
    WeaponRange currentWeapon;
    int currentWeaponIdx;
    PoolBase pool;

    List<InputTap> swapToSlotInputs = new List<InputTap>();

    void Debugg()
    {
        Debug.Log("asd");
    }

    void Reload()
    {
        currentWeapon.Reload();
    }

    void SwapToInput(int idx)
    {
        if (currentWeaponIdx == idx)
        {
            return;
        }
        
        currentWeapon.Disable();
        currentWeapon.OnShootEmpty.RemoveListener(Reload);

        currentWeaponIdx = idx;
        currentWeapon = weapons[currentWeaponIdx];
        currentWeapon.Enable();
        currentWeapon.OnShootEmpty.AddListener(Reload);
    }

    #endregion

}

[System.Serializable]
public class WeaponSpawnDesc
{
    public Transform transform;
    public GameObject prefab;
}
