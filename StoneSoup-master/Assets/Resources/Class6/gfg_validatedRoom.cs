using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gfg_validatedRoom : Room
{
    public bool has_up_exit;
    public bool has_right_exit;
    public bool has_down_exit;
    public bool has_left_exit;

    public bool has_up_right_path;
    public bool has_up_left_path;
    public bool has_up_down_path;
    public bool has_left_right_path;
    public bool has_down_right_path;
    public bool has_down_left_path;

    public bool obeysExitConstraints(ExitConstraint constraint){
        if (constraint.upExitRequired && !has_up_exit){
            return false;
        }
        if (constraint.rightExitRequired && !has_right_exit){
            return false;
        }
        if (constraint.leftExitRequired && !has_left_exit){
            return false;
        }
        if (constraint.downExitRequired && !has_down_exit){
            return false;
        }
        if (constraint.upExitRequired && constraint.rightExitRequired && !has_up_right_path){
            return false;
        }
        if (constraint.upExitRequired && constraint.downExitRequired && !has_up_down_path){
            return false;
        }
        if (constraint.upExitRequired && constraint.leftExitRequired && !has_up_left_path){
            return false;
        }
        if (constraint.leftExitRequired && constraint.rightExitRequired && !has_left_right_path){
            return false;
        }
        if (constraint.downExitRequired && constraint.rightExitRequired && !has_down_right_path){
            return false;
        }
        if (constraint.downExitRequired && constraint.leftExitRequired && !has_down_left_path){
            return false;
        }
        return true;
    }
}
