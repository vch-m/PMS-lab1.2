package com.vchm;
import javax.xml.crypto.Data;
import java.util.Date;

public class TimeVM{
    public int hours;
    public int minutes;
    public int seconds;

    public TimeVM(){
        hours = 0;
        minutes = 0;
        seconds = 0;
    }

    public TimeVM(int h, int m, int s){
        if (h >= 0& h <= 23){
            hours = h;
        }
        else{
            throw new IllegalArgumentException("Часы указано неверно\n");
        }
        if (m >= 0 & m <= 59){
            minutes = m;
        }
        else{
            throw new IllegalArgumentException("Минуты указано неверно\n");
        }
        if (s >= 0 & s <= 59){
            seconds = s;
        }
        else{
            throw new IllegalArgumentException("Секунды указано неверно\n");
        }
    }
    public TimeVM (Date d){
        hours = d.getHours();
        minutes = d.getMinutes();
        seconds = d.getSeconds();
    }

    public String getFormattedTime(){
        String zz = "";
        int zHours;
        if(hours == 0){
            zHours = 12;
            zz = "AM";
        }
        else if (hours == 12){
            zHours = 12;
            zz = "PM";
        }
        else if (hours < 13){
            zHours = hours;
            zz = "AM";
        }
        else{
            zHours = hours - 12;
            zz = "PM";
        }
        //String str = String.format("%n:%n:%n %s", zHours, minutes, seconds, zz);
        String str = formatNumber(zHours)+":"+formatNumber(minutes)+":"+formatNumber(seconds)+" "+zz;
        return str;
    }
    private String formatNumber(int i){
        String s = String.valueOf(i);
        if (s.length() == 1){
            s = "0"+s;
        }
        return s;
    }
    public static TimeVM Add(TimeVM a, TimeVM b){
        int s = a.seconds + b.seconds;
        int m = a.minutes + b.minutes;
        int h = a.hours + b.hours;

        if (s > 59){
            s = s - 60;
            m = m + 1;
        }
        if (m > 59){
            m = m - 60;
            h = h + 1;
        }
        if (h > 23){
            h = h - 24;
        }

        TimeVM result = new TimeVM(h, m, s);
        return result;
    }
    public TimeVM Add(TimeVM a){
        return Add(this, a);
    }

    public static TimeVM Subtract(TimeVM a, TimeVM b){
        int s = a.seconds - b.seconds;
        int m = a.minutes - b.minutes;
        int h = a.hours - b.hours;

        if (s < 0){
            s = 60 + s;
            m = m - 1;
        }
        if (m < 0){
            m = 60 + m;
            h = h - 1;
        }
        if (h < 0){
            h = 24 + h;
        }

        TimeVM result = new TimeVM(h, m, s);
        return result;
    }
    public TimeVM Subtract(TimeVM a){
        return Subtract(this, a);
    }

}

