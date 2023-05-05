import React, { Component, useState } from "react";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import InputAdornment from "@material-ui/core/InputAdornment";
import Icon from "@material-ui/core/Icon";
// @material-ui/icons
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
import Link from "next/link";
import { Dialog, DialogActions, DialogContent, DialogTitle, IconButton, Slide } from "@material-ui/core";
import { Close } from "@material-ui/icons";
import { ENDPOINTS, createAPIEndpoint } from "../api";
import Router from "next/router";

const useStyles = makeStyles(styles);
const Transition = React.forwardRef(function Transition(props, ref) {
  return <Slide direction="down" ref={ref} {...props} />;
});
export default function LoginPage(props) {
  const [cardAnimaton, setCardAnimation] = React.useState("cardHidden");
  const [userName, setUserName] = useState("")
  const [password, setPassword] = useState("")
  const [showError, setshowError] = useState(true)
  const [errorName, setErrorName] = useState(false)
  const [errorPassword, setErrorPassword] = useState(false)
  const [modal, setModal] = useState(false);
  const [notification, setNotification] = useState(false)
  const hadleInputUserNameChange = e =>{
    const {value} = e.target
    setUserName(value)
  }
  const hadleInputPassword= e =>{
    const {value} = e.target
    setPassword(value)
  }
  setTimeout(function () {
    setCardAnimation("");
  }, 700);
  const classes = useStyles();
  const { ...rest } = props;
  const Login = () => {
    if(userName === "")
    {
      setErrorName(true);
      setNotification("Username cannot empty")
      setshowError(false)
    }
    else if (password == "")
    {
      setErrorPassword(true);
      setNotification("Password cannot empty")
      setshowError(false)
    }
    else
    {
      createAPIEndpoint(ENDPOINTS.login)
      .getLogin(
        {
          userName: userName,
          password: password
        }
      ).then(res =>{
          if(res.data.status === "Username not found" || res.data.status  === "Password not correct")
          {
            setModal(true);
            setNotification(res.data.status)
          }
          else
          {
            Router.push('/all-Clothes/');
          }        
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
                    <h4>Login</h4>
                  </CardHeader>
                  <CardBody>
                    <CustomInput
                      labelText="User Name..."
                      id="Username"
                      error={errorName} 
                      formControlProps={{
                        fullWidth: true
                      }}
                      inputProps={{
                        type: "text",
                        onChange: hadleInputUserNameChange,
                        endAdornment: (
                          <InputAdornment position="end">
                            <People className={classes.inputIconsColor} />
                          </InputAdornment>
                        )
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
                        onChange: hadleInputPassword,
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
                      <Button simple color="primary" size="lg" onClick = {Login}>
                        Login
                      </Button>
                    <Link href='/createAccount/'>
                      <Button simple color="primary" size="lg">
                        Create Account
                      </Button>
                    </Link>
                  </CardFooter>
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
                        <Button color="danger"onClick={() => setModal(false)}>
                          Yes
                        </Button>
                      </DialogActions>
                    </Dialog>
                </form>
              </Card>
            </GridItem>
          </GridContainer>
        </div>
        <Footer whiteFont />
      </div>
    </div>
  );
}
