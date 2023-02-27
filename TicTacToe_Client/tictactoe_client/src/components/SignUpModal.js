import {useRef} from 'react';
import { useNavigate } from 'react-router-dom';

import {Requests} from '../Requests'

function SignUpModal(props) {
    const usernameInputRef = useRef();
    const passwordInputRef = useRef();
    const repeatPasswordInputRef = useRef();
    const navigate = useNavigate();

    async function signUpHandler() {
        let usernameInput = usernameInputRef.current.value;
        let passwordInput = passwordInputRef.current.value;
        let repeatPasswordInput = repeatPasswordInputRef.current.value;

        if (usernameInput.trim() !== "" && passwordInput.trim() !== "" && passwordInput === repeatPasswordInput)
        {
            let requestResult = await Requests.SignUpRequest({username : usernameInput, password : passwordInput});
            requestResult = requestResult.replaceAll('\"', '');

            if (requestResult !== "00000000-0000-0000-0000-000000000000")
            {
                Requests.userGuid = requestResult;
                props.onClose();
                navigate('/matches');
            }
            else
            {
            }
        }
    }

    return (
        <div className='modal'>
            <div className='input'>
            <label htmlFor='UsernameInput'>Username:</label>
            <input type="text" id="UsernameInput" name="Username" required
                minLength="4" maxLength="8" size="10" ref={usernameInputRef}/>
            </div>    

            <div className="input">
            <label htmlFor='PasswordInput'>Password:</label>
            <input type="password" id="PasswordInput" name="Password" required
                minLength="4" maxLength="8" size="10" ref={passwordInputRef}/>
            </div>

            <div className="input">
            <label htmlFor='RepeatPasswordInput'>Repeat Password:</label>
            <input type="password" id="RepeatPasswordInput" name="Password" required
                minLength="4" maxLength="8" size="10" ref={repeatPasswordInputRef}/>
            </div>

            <button className="btn" onClick={signUpHandler}>Sign Up</button>
        </div>
    );
}

export default SignUpModal;