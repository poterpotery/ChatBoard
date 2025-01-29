/**
 * This file contains the application's variables.
 *
 * Define color, sizes, etc. here instead of duplicating them throughout the components.
 * That allows to change them more easily later on.
 */
/**
 * Colors
 */
export const Colors = {
  primaryColor: '#7FCE00',
  secondaryColor: '#9DFF00',
};
export const NavigationColors = {
  primary: Colors.primaryColor,
  background: '#EFEFEF',
  card: '#EFEFEF',
};
/**
 * FontSize
 */
export const FontSize = {
  extraSmall: 10,
  small: 12,
  medium: 14,
  regular: 16,
  large: 18,
  extraLarge: 20,
};
/*
 *  Borders
 */
export const borders = {
  normalBorder: 10,
  mediumBorder: 25,
  fullBorder: 100,
};

/**
 * Metrics Sizes
 */
const tiny = 10;
const small = tiny * 2; // 20
const regular = tiny * 3; // 30
const large = regular * 2; // 60
export const MetricsSizes = {
  tiny,
  small,
  regular,
  large,
};
export const constants = {
  FLATLIST_PADDING_BOTTOM: 90,
};
export default {
  Colors,
  NavigationColors,
  FontSize,
  MetricsSizes,
  borders,
  constants,
};
