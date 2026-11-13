import pytest

from bloodpressure import ADVICE, BloodPressure, BPCategory


@pytest.mark.parametrize("systolic, diastolic, expected", [
    (70, 40, BPCategory.LOW),
    (89, 59, BPCategory.LOW),
    (90, 59, BPCategory.IDEAL),
    (89, 60, BPCategory.IDEAL),
    (100, 60, BPCategory.IDEAL),
    (119, 79, BPCategory.IDEAL),
    (120, 70, BPCategory.PRE_HIGH),
    (110, 80, BPCategory.PRE_HIGH),
    (139, 89, BPCategory.PRE_HIGH),
    (140, 70, BPCategory.HIGH),
    (130, 90, BPCategory.HIGH),
    (190, 100, BPCategory.HIGH),
])
def test_category(systolic, diastolic, expected):
    assert BloodPressure(systolic, diastolic).category == expected


def test_category_display_names():
    assert [c.value for c in BPCategory] == [
        "Low Blood Pressure", "Ideal Blood Pressure",
        "Pre-High Blood Pressure", "High Blood Pressure"]


@pytest.mark.parametrize("systolic, diastolic, expected", [
    (85, 55, "See a doctor if you feel dizzy."),
    (110, 70, "Keep up the good work!"),
    (125, 82, "Eat less salt and be more active."),
    (145, 92, "Please see a doctor."),
])
def test_advice(systolic, diastolic, expected):
    assert expected in BloodPressure(systolic, diastolic).advice


def test_every_category_has_advice():
    assert set(ADVICE) == set(BPCategory)
