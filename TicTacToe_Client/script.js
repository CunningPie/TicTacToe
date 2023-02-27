let playerSymbol = "X";
let gameEnded = false;
let yourTurn = true;

for (let i = 1; i <= 9; i++) {
    document.getElementById(i.toString()).addEventListener(
        "click", 
        function() {
            if (this.innerHTML === "" && yourTurn && !gameEnded) {
                this.innerHTML = playerSymbol;
                this.classList.add(playerSymbol.toLowerCase());
                yourTurn = !yourTurn

                if (playerSymbol === "X")
                    playerSymbol = "O"
                else
                    playerSymbol = "X"
            }


        }
    );
}

document.getElementById("SignIn").addEventListener(
    "click", 
    function() {
        var username = document.getElementById("UsernameInput").value;
        var password = document.getElementById("PasswordInput").value;

        if (username.trim() != "" && password.trim() != "")
        {
            var credentials = btoa(username + ":" + password);
            var auth = { "Authorization" : `Basic ${credentials}` }; 
            fetch('https://localhost:7241/Auth', { headers : auth })
            .then(response => {
                if (response.status == '200')
                {
                    alert("Logged In")
                    document.getElementById("User").innerHTML = username;

                    document.getElementById("UsernameLabel").style.display = 'none';
                    document.getElementById("UsernameInput").style.display = 'none';
                    document.getElementById("PasswordLabel").style.display = 'none';
                    document.getElementById("PasswordInput").style.display = 'none';
                    document.getElementById("SignIn").style.display = 'none';
                }
                else
                {
                    alert("Invalid username or password")
                }
            });
        }
    }
);

    