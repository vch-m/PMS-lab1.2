package com.vchm;
import java.util.Date;

import java.sql.Time;

public class Main {

    public static void main(String[] args) {
        TimeVM value = new TimeVM(4, 46, 17);
        String formTime = value.getFormattedTime();
        System.out.println("Результат №6.a: " + formTime);

        TimeVM value2 = new TimeVM(12, 23, 59);
        TimeVM sum = value.Add(value2);
        System.out.println("Результат №6.b: " + sum.getFormattedTime());

        TimeVM value3 = new TimeVM(2, 23, 59);
        TimeVM diff = value.Subtract(value3);
        System.out.println("Результат №6.c: " + diff.getFormattedTime());

        TimeVM value4 = new TimeVM(2, 23, 59);
        TimeVM sum2 = value.Add(value, value2);
        System.out.println("Результат №7.a: " + sum2.getFormattedTime());

        TimeVM value5 = new TimeVM(2, 23, 59);
        TimeVM diff2 = value.Subtract(value, value3);
        System.out.println("Результат №7.b: " + diff2.getFormattedTime());

        TimeVM value6 = new TimeVM();
        TimeVM value7 = new TimeVM(23, 59, 59);
        Date date = new Date(2021, 03, 13, 13, 15, 10);
        TimeVM value8 = new TimeVM(date);
        System.out.println("Результат №8: ");
        System.out.println("    " + value6.getFormattedTime());
        System.out.println("    " + value7.getFormattedTime());
        System.out.println("    " + value8.getFormattedTime());

        TimeVM value9 = new TimeVM(23, 59, 59);
        TimeVM value10 = new TimeVM(12, 00, 01);
        TimeVM sum3 = value9.Add(value10);
        System.out.println("Результат №10.a: " + sum3.getFormattedTime());

        TimeVM value11 = new TimeVM(00, 00, 00);
        TimeVM value12 = new TimeVM(00, 00, 01);
        TimeVM sub3 = value11.Subtract(value12);
        System.out.println("Результат №10.b: " + sub3.getFormattedTime());
    }
}
