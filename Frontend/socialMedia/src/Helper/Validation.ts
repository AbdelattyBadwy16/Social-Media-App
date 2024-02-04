

export function CheckName(name : string) {
    if (name.length) {
        return 1;
    } else return 0;
}

export function CheckPassword(pass : string) {
    
    let bigChar = false;
    let smallChar = false;
    let num = false;
    let symbol = false;

    for (let i = 0; i < pass.length; i++) {
        if (pass[i] >= 'a' && pass[i] <= 'z') smallChar = true;
        else if (pass[i] >= 'A' && pass[i] <= 'Z') bigChar = true;
        else if (pass[i] >= '0' && pass[i] <= '9') num = true;
        else symbol = true;
    }


    if( bigChar && smallChar && num && symbol) return 1 ;
    else return 0;
}


