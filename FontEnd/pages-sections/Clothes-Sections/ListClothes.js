import React, { useEffect, useState } from "react";
// nodejs library that concatenates classes
import classNames from "classnames";
// @material-ui/core components
import { makeStyles } from "@material-ui/core/styles";
// core components
import GridContainer from "/components/Grid/GridContainer.js";
import GridItem from "/components/Grid/GridItem.js";
import Card from "/components/Card/Card.js";
import CardBody from "/components/Card/CardBody.js";
import styles from "/styles/jss/nextjs-material-kit/pages/landingPageSections/teamStyle.js";
//API
import { ENDPOINTS, createAPIEndpoint } from "../../api/index.js";
const useStyles = makeStyles(styles);
const BASE_URL = "/img/clothes/"
export default function ListClothes() {
  const classes = useStyles();
  const [clothes, setClothes] = useState([])
  const imageClasses = classNames(
    classes.imgRaised,
    classes.imgFluid
  );
  useEffect(()=> {
    createAPIEndpoint(ENDPOINTS.getAllClothes)
    .fetch()
    .then(res => {
        setClothes(res.data)
    })
  },[])
  const TurnOSSOn = (item) =>
  {
    if(item.quantity == 0)
    {
      document.querySelector('div[name="'+item.id+'"]').hidden = false;
    }
  }
  const TurnOSSOff = (item) =>
  {
    document.querySelector('div[name="'+item.id+'"]').hidden = true;
  }
  return (
    <div className={classes.section}>
      <h2 className={classes.title}>Here is our clothes</h2>
      <div>
        <GridContainer>
          {clothes.map(item => {
            return (
                <GridItem xs={12} sm={12} md={3} key={item.clothesName}>
                  <Card plain>
                    <GridItem xs={12} sm={12} md={6} className={classes.itemGrid} key={item.clothesName}>
                      <a href={"/Detail/"+item.clothesName +"-"+ item.id}>
                      <img
                        src= {BASE_URL + item.image}
                        alt="..."
                        className={imageClasses}
                        onMouseEnter={() => TurnOSSOn(item)}
                        onMouseLeave={() => TurnOSSOff(item)}
                      />
                      </a>
                      <div
                      hidden={true}
                      name={item.id} 
                      style={{
                        position: "absolute",
                        top: 0,
                        bottom: 0,
                        left: 0,
                        right: 0,
                        height: "100%",
                        width: "100%",
                        opacity: 1,
                        transition: ".5s ease",
                        backgroundColor: "red",
                      }}>
                        <div style={{textAlign: "center", fontSize:"20px", margin:"25%"}}>Out of stock</div>
                      </div>
                    </GridItem>
                    <a href={"/Detail/"+item.clothesName}>
                    <h4 className={classes.cardTitle}>{item.clothesName}</h4>
                    </a>
                    <h4 className={classes.cardTitle}>
                    <small className={classes.smallTitle}>Size: {item.size}</small>
                    </h4>
                    <CardBody>
                      <p className={classes.description}>
                      {item.prices}.000 VNĐ
                      </p>
                    </CardBody>
                  </Card>
                </GridItem>
            )})}
        </GridContainer>
      </div>
    </div>
  );
}
