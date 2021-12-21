
import React from 'react';
import { Button, makeStyles } from '@material-ui/core';
import Cues from '../../components/Cues';
import Chalks from '../../components/Chalks';
import Hall from '../../components/Hall';
import LifesAndTokens from '../../components/LifesAndTokens';
import styles from './style';

const useStyles = makeStyles(styles);

function HomePage() {
  const classes = useStyles();
  
  const [selected, setSelected] = React.useState(0);

  const images = [
    "cues.png",
    "chalks.png",
    "hall.png",
    "lifes.png",
  ];

  const titles = [
    "Cues",
    "Chalks",
    "Hall Design",
    "Lifes & Tokens",
  ];

  return (
    <>
      <div className={classes.hero}>
        <Button className={classes.circle_btn}>
          {'<'}
        </Button>
        <div className={classes.buttons}>
          {images.map((img, index) => (
            <Button key={index} className={`${classes.btn} ${selected === index ? classes.selected_btn : ''}`} onClick={() => setSelected(index)}>
              <img src={"/images/" + img} alt="" />
              {titles[index]}
            </Button>
          ))}
        </div>
        { selected === 0 && <Cues /> }
        { selected === 1 && <Chalks /> }
        { selected === 2 && <Hall /> }
        { selected === 3 && <LifesAndTokens /> }
      </div>
    </>
  );
}

export default HomePage;