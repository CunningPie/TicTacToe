import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Requests } from "../Requests";

let intervalId;

function Field() {
    const navigate = useNavigate();
    const [yourTurn, setYourTurn] = useState(Requests.yourTurnInit);
    const [gameEnded, setGameEnded] = useState('0');
    const playerSymbol = Requests.Sym;


    useEffect(() => {       
        console.log(gameEnded);
        if (gameEnded !== '0') {    
            if (gameEnded === '1'){
                alert('You win!');            
            }
            if (gameEnded === '2'){
                alert('Draw!');            
            }
            else {

            }
            navigate('/matches');
            clearInterval(intervalId);
        }

        if (yourTurn)
        {
            clearInterval(intervalId)
        }
        else {
            intervalId = setInterval(async () => {
                let turnStatus = await Requests.CheckTurn();

                if (turnStatus !== '0' && turnStatus !== '1')
                {
                    if (turnStatus === '-1')
                    {
                        alert('You lose!');
                    }
                    if (turnStatus === '2')
                    {
                        alert('You win!');
                    }
                    if (turnStatus === '3')
                    {
                        alert('Draw!');
                    }

                    clearInterval(intervalId);
                    navigate('/matches');

                }


                setYourTurn(turnStatus === '1');
                FieldRefresh(await Requests.RefreshField());

            }, 1000);
        }

    }, [yourTurn]);

    async function fieldClickHandler(event) {
        if (event.target.innerHTML === "" && yourTurn && gameEnded !== 'true') {
            event.target.innerHTML = playerSymbol;
            event.target.classList.add(playerSymbol.toLowerCase());

            let x = Math.floor((event.target.id - 1) / 3);
            let y = event.target.id - x * 3 - 1;

            setGameEnded(await Requests.SendTurn({x, y}));

            if (gameEnded !== '0')
            {
                if (gameEnded === '1'){
                    alert('You win!');            
                }
                else if (gameEnded === '2'){
                    alert('Draw!');            
                }

                navigate('/matches');
            }

            setYourTurn(!yourTurn);

        }
    }

    function FieldRefresh(strProps){
            [...document.querySelectorAll('table tbody td')].forEach((x, index) => {
                if (strProps[index] === '1') {
                    x.innerHTML = 'X';     
                    x.className = 'x';
                }
                else if (strProps[index] === '2')
                {
                    x.innerHTML = 'O';
                    x.className = 'o';
                }
                else {
                    x.innerHTML = '';
                }
            })
    }

    return (
    <div>
        <table onClick={fieldClickHandler}>
            <tbody>
                <tr>
                <td id="1"></td>
                <td id="2"></td>
                <td id="3"></td>
                </tr>
                <tr>
                <td id="4"></td>
                <td id="5"></td>
                <td id="6"></td>
                </tr>
                <tr>
                <td id="7"></td>
                <td id="8"></td>
                <td id="9"></td>
                </tr>
            </tbody>
        </table>
    </div>
    );
  }
  
  export default Field;