/*eslint-disable*/
import React from "react";

// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
// @material-ui/icons
import { Search,AddShoppingCartSharp } from "@material-ui/icons";
// core components
import Router from "next/router";
import CustomInput from "/components/CustomInput/CustomInput.js";
import styles from "/styles/jss/nextjs-material-kit/components/headerLinksStyle.js";
import { Button } from "@material-ui/core";

const useStyles = makeStyles(styles);

export default function HeaderLinks(props) {
  const classes = useStyles();
  const goToCartPage = () => {
    Router.push('/Cart');
  }
  return (
    <List className={classes.list}>
      <ListItem className={classes.list}>
        <CustomInput
            labelText="Search"
            id="search"
            formControlProps={{
                fullWidth: true
            }}
            inputProps={{
                endAdornment: (<Button position="end"><Search/></Button>)
            }}
        />
        <Button justicon="true" round="true" color="primary" onClick={goToCartPage}><AddShoppingCartSharp style={{color: "#FFFFFF"}}/></Button>
      </ListItem>
    </List>
  );
}
