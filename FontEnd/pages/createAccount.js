import React, { useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
import Icon from "@material-ui/core/Icon";
// @material-ui/icons
import Email from "@material-ui/icons/Email";
import People from "@material-ui/icons/People";
// core components
import Header from "/components/Header/Header.js";
import HeaderLinks from "/components/Header/HeaderLinks.js";
import Footer from "/components/Footer/Footer.js";
import GridContainer from "/components/Grid/GridContainer.js";
import GridItem from "/components/Grid/GridItem.js";
import Button from "/components/CustomButtons/Button.js";
import Card from "/components/Card/Card.js";
import CardBody from "/components/Card/CardBody.js";
import CardHeader from "/components/Card/CardHeader.js";
import CardFooter from "/components/Card/CardFooter.js";
import CustomInput from "/components/CustomInput/CustomInput.js";
import styles from "/styles/jss/nextjs-material-kit/pages/loginPage.js";
import { Dialog, DialogActions, DialogContent, DialogTitle, IconButton, Link, Slide } from "@material-ui/core";
import { Close } from "@material-ui/icons";
import { ENDPOINTS, createAPIEndpoint } from "../api";
import Router from "next/router";
const useStyles = makeStyles(styles);
const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="down" ref={ref} {...props} />;
});
export default function CreateAccountPage(props) {
    const [cardAnimaton, setCardAnimation] = useState("cardHidden");
    const { ...rest } = props;
    const [errorName, setErrorName] = useState(false)
    const [errorEmail, setErrorEmail] = useState(false)
    const [errorPassword, setErrorPassword] = useState(false)
    const [notification, setNotification] = useState(false)
    const [showError, setshowError] = useState(true)
    const [modal, setModal] = useState(false);
    const [hiddenVerification, setHiddenVerification] = useState(false)
    const [verificationCode, setVerificationCode] = useState(0)
    const validate = (email) => {
      let temp = {}
      temp = (/\S+@\S+\.\S+/).test(email)?true:false
      setErrorEmail(!temp);
      return temp;
  }
    setTimeout(function () 
    {
      setCardAnimation("");
    }, 700);
    const classes = useStyles();
    const SendVerificationCode = (verificationCode) => {

      var email = document.querySelector('input[id=Email]').value;
      createAPIEndpoint(ENDPOINTS.Email)
      .fetchWithEmail(email,verificationCode)
      .then(res => {
          if (res.data === true)
          alert("Send verification code to email successful");
      })
    }
    const createAccount = () => {
      var userName = document.querySelector('input[id=Username]').value;
      var password = document.querySelector('input[id=pass]').value;
      var email = document.querySelector('input[id=Email]').value;
      var code = document.querySelector('input[id=verification]').value;
      if (code !== "")
      {
        createAPIEndpoint(ENDPOINTS.createAccount)
        .postCreateAccount(
          {
            userName: userName,
            email: email,
            password: password,
            VerificationCodeFromUser: code,
            VerificationCodeFromProgram:verificationCode.toString()
          }
        ).then(res =>{
            if(res.data === false)
            {
              setNotification("Verification code does not match")  
            }
            else
            {
              setNotification("Create account successful")
              setModal(true);
              setHiddenVerification(true)
              setTimeout(function () 
              {
                Router.push("/login/")
              }, 900);
            }
        })
      }
      else
      {
        setNotification("Verification code cannot be empty")
      }
      setModal(true);
    }
    const checkAccount = () => {
      var userName = document.querySelector('input[id=Username]').value;
      var password = document.querySelector('input[id=pass]').value;
      var email = document.querySelector('input[id=Email]').value;
      var confirmPassword = document.querySelector('input[id=confirmPass]').value;
      if(userName === "")
      {
        setErrorName(true);
        setNotification("Username cannot empty")
        setshowError(false)
      }
      else if (!validate(email))
      {
        setNotification("Email not validate")
        setshowError(false)
      }
      else if (password != confirmPassword || password == "")
      {
        setErrorPassword(true);
        setNotification("Password doesn't match")
        setshowError(false)
      }
      else
      {
        createAPIEndpoint(ENDPOINTS.account)
        .fetchWithUsername(userName).then(res =>{
            if(res.data === false)
            {
              setNotification("Username already exits")
              setHiddenVerification(true)
            }
            else
            {
              setNotification("Verification code has send to your email")
              setHiddenVerification(false)
              var randomNumber = Math.floor(1000 + Math.random() * 9000);
              console.log(randomNumber)
              setVerificationCode(randomNumber);
              SendVerificationCode(randomNumber);
            }
            setModal(true);
        })
      }
    }
    return (
      <div>
        <Header
          absolute
          color="transparent"
          brand="NextJS Material Kit"
          rightLinks={<HeaderLinks />}
          {...rest}
        />
        <div
          className={classes.pageHeader}
          style={{
            backgroundImage: "url('/img/bg7.jpg')",
            backgroundSize: "cover",
            backgroundPosition: "top center"
          }}
        >
          <div className={classes.container}>
            <GridContainer justify="center">
              <GridItem xs={12} sm={6} md={4}>
                <Card className={classes[cardAnimaton]}>
                  <form className={classes.form}>
                    <CardHeader color="primary" className={classes.cardHeader}>
                      <h4>Create Account</h4>
                    </CardHeader>
                    <CardBody>
                      <CustomInput
                        labelText="User Name..."
                        id="Username"
                        error={errorName}                     
                        formControlProps={{
                          fullWidth: true,
                        }}
                        inputProps={{
                          type: "text",
                          endAdornment: (
                            <InputAdornment position="end">
                              <People className={classes.inputIconsColor} />
                            </InputAdornment>
                          ),
                          autoComplete: "off"
                        }}
                      />
                       <CustomInput
                        labelText="Email..."
                        id="Email"
                        error={errorEmail}     
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "email",
                          endAdornment: (
                            <InputAdornment position="end">
                              <Email className={classes.inputIconsColor} />
                            </InputAdornment>
                          ),
                          autoComplete: "off"
                        }}
                      />
                      <CustomInput
                        labelText="Password"
                        id="pass"
                        error={errorPassword}     
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "password",
                          endAdornment: (
                            <InputAdornment position="end">
                              <Icon className={classes.inputIconsColor}>
                                lock_outline
                              </Icon>
                            </InputAdornment>
                          ),
                          autoComplete: "off"
                        }}
                      />
                      <CustomInput
                        labelText="ConfirmPassword"
                        id="confirmPass"
                        error={errorPassword}     
                        formControlProps={{
                          fullWidth: true
                        }}
                        inputProps={{
                          type: "password",
                          endAdornment: (
                            <InputAdornment position="end">
                              <Icon className={classes.inputIconsColor}>
                                lock_outline
                              </Icon>
                            </InputAdornment>
                          ),
                          autoComplete: "off"
                        }}
                      />
                    </CardBody>
                    
                    <h4 style={{color: "red",marginLeft: 35}} hidden={showError}>
                        {notification}
                    </h4>
                    <CardFooter className={classes.cardFooter}>
                      <Link href="/login">
                          <Button simple color="primary" size="lg">
                            Cancel
                          </Button>
                        </Link>
                        <Button simple color="primary" size="lg" onClick={checkAccount}>
                          Create Account
                        </Button>
                    </CardFooter>
                  </form>

                  <Dialog
                      classes={{
                        root: classes.center,
                        paper: classes.modal
                      }}
                      open={modal}
                      TransitionComponent={Transition}
                      keepMounted
                      onClose={() => setModal(false)}
                      aria-labelledby="modal-slide-title"
                      aria-describedby="modal-slide-description"
                    >
                      <DialogTitle
                        id="classic-modal-slide-title"
                        disableTypography
                        className={classes.modalHeader}
                      >
                        <IconButton
                          className={classes.modalCloseButton}
                          key="close"
                          aria-label="Close"
                          color="inherit"
                          onClick={() => setModal(false)}
                        >
                          <Close className={classes.modalClose} />
                        </IconButton>
                        <h4 className={classes.modalTitle}>Notification</h4>
                      </DialogTitle>
                      <DialogContent
                        id="modal-slide-description"
                        className={classes.modalBody}
                      >
                        <h5>{notification}</h5>
                      </DialogContent>
                      <DialogActions
                        className={classes.modalFooter + " " + classes.modalFooterCenter}
                      >
                         <div hidden = {hiddenVerification} style={{width:"100%"}}>
                            <CustomInput
                            labelText="Verification Code"
                            id="verification"
                            
                            error={errorPassword}     
                            formControlProps={{
                              fullWidth: true
                            }}
                            inputProps={{
                              type: "text",
                            }}
                            />
                        </div>
                      </DialogActions>
                      <div hidden = {hiddenVerification} style={{marginLeft:"22%"}}>
                        <Button id="sendEmail"  simple color="primary" size="lg"  onClick= {createAccount}>
                              Confirm verification Code
                        </Button>
                      </div>
                    </Dialog>
                </Card>
              </GridItem>
            </GridContainer>
          </div>
          <Footer whiteFont />
        </div>
      </div>
    );
}