using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TabelaDano {

    public static EnumDamange GetDemangeByST(int st) {
        switch (st) {
            case 1:
                return EnumDamange.d1Minus5;
            case 2:
                return EnumDamange.d1Minus5;
            case 3:
                return EnumDamange.d1Minus4;
            case 4:
                return EnumDamange.d1Minus4;
            case 5:
                return EnumDamange.d1Minus3;
            case 6:
                return EnumDamange.d1Minus3;
            case 7:
                return EnumDamange.d1Minus2;
            case 8:
                return EnumDamange.d1Minus2;
            case 9:
                return EnumDamange.d1Minus1;
            case 10:
                return EnumDamange.d1;
            case 11:
                return EnumDamange.d1Add1;
            case 12:
                return EnumDamange.d1Add2;
            case 13:
                return EnumDamange.d2Minus1;
            case 14:
                return EnumDamange.d2;
            case 15:
                return EnumDamange.d2Add1;
            case 16:
                return EnumDamange.d2Add2;
            case 17:
                return EnumDamange.d3Minus1;
            case 18:
                return EnumDamange.d3;
            default:
                break;
        }
        return EnumDamange.d1Minus5;
    }
}
