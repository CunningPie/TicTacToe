import {useState} from 'react';

import SignUpModal from './SignUpModal';
import Backdrop from './Backdrop';

function SignUpButton() {
    const [signUpModalIsOpen, setSignUpModalIsOpen] = useState(false);

    function openSignUpModalHandler() {
        setSignUpModalIsOpen(true);
    }

    function closeSignUpModalHandler(){
        setSignUpModalIsOpen(false)
    }

    return (
        <div>
            <div>
                <button className="btn" onClick={openSignUpModalHandler}>
                    Sign Up
                </button>
            </div>
        {signUpModalIsOpen && <SignUpModal onClose={closeSignUpModalHandler}/>}
        {signUpModalIsOpen && <Backdrop onClose={closeSignUpModalHandler}/>}
        </div>
    );
}
  
export default SignUpButton;