### README: **משחק ניווט מבוסס אריחים**

---

## **תיאור המשחק**

המשחק הוא משחק ניווט מבוסס אריחים שבו השחקן שולט בדמות שנעה על רשת אריחים (Tilemap) כדי להגיע למיקום יעד שנקבע מראש ע"י לחיצה בעזרת העכבר על המיקום או לחלופין תזוזה דינמית בעזרת חצי המקלדת. המשחק כולל לוגיקת חישוב מסלול מתקדמת, מגבלות תנועה ריאליסטיות, ומערכת דינמית של הסתרת אריחים לשיפור החוויה והאתגר.

---

## **מטרת המשחק**
- המטרה המרכזית במשחק היא לטייל עם הדמות של השחקן מנקודת ההתחלה על מפת האריחים, תוך התגברות על מכשולים (כמו הרים).

---

## **איך לשחק**

1. **קביעת יעד:**
   - בוחרים עם העכבר מקום אליו רוצים להגיע במפה או לחלופין זזים במפה בעזרת החצים במקלדת.

2. **נווטו במפה:**
   - הדמות תחשב אוטומטית את המסלול הקצר ביותר ותתחיל לנוע.

3. **שימו לב למכשולים:**
   - אריחים בלתי עבירים (כמו הרים) יחסמו את הדרך, ויידרש למצוא מסלול חלופי.

4. **גלו אזורים מוסתרים:**
   - תצוגת המפה תושפע ממיקום השחקן ביחס להרים.

---

## **היבטים טכניים**

### **מבוסס על מפת האריחים של יוניטי**
- מנצל את מערכת האריחים של Unity ליצירת מפה יעילה ומדויקת.

### **חישוב מסלול חכם**
- האלגוריתם BFS מבטיח ניווט מהיר ויעיל גם במפות מורכבות.

### **הסתרת אריחים**
- אריחים מוסתרים בהתבסס על מיקום השחקן והפרספקטיבה.

### **אפשרויות התאמה אישית**
- התאמה של מהירות התנועה, מגבלות האריחים, והאלגוריתם.

---


